using Ardalis.Result;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ProyectoFinal.Data;
using ProyectoFinal.Data.Email;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.ServicesContracts;
using System.Text.Json;

namespace ProyectoFinal.Handlers.UserHanlders
{
    public class CreateUserHandler : IRequestHandler<CreateUserRequest, Result<User>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateUserHandler> _logger;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IEmailTasksService emailTasksService;

        public CreateUserHandler(IMapper mapper,
            ILogger<CreateUserHandler> logger,
            UserManager<User> userManager,
            IUnitOfWork unitOfWork,
            IEmailTasksService emailTasksService)
        {
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;
            this.unitOfWork = unitOfWork;
            this.emailTasksService = emailTasksService;
        }

        public async Task<Result<User>> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Consultando repositorio: Request:{JsonSerializer.Serialize(request)}");
                unitOfWork.BeginTransaction();
                var user = _mapper.Map<User>(request);
                var result = await _userManager.CreateAsync(user, request.Pass);
                if (result.Succeeded)
                {
                    _logger.LogInformation($"Respuesta Handler:{JsonSerializer.Serialize(user)}");
                    unitOfWork.CommitTransaction();
                    var emailTask = new EmailTask
                    {
                        Data = JsonSerializer.Serialize(new { UserId = user.Id }),
                        Type = "ConfirmEmail"
                    };
                    await emailTasksService.CreateTask(JsonSerializer.Serialize(emailTask));
                    return Result<User>.Success(user);
                }
                unitOfWork.RollBack();
                _logger.LogInformation($"Respuesta Handler:{JsonSerializer.Serialize(user)}");
                return Result<User>.Invalid(result.Errors.Select(e => new ValidationError() { ErrorCode = e.Code, ErrorMessage = e.Description }).ToList());
            }
            catch (Exception ex)
            {
                unitOfWork.RollBack();
                _logger.LogError("Error! : {0} {1}", ex.Message, ex.StackTrace);
                _logger.LogInformation($"Respuesta Handler:{JsonSerializer.Serialize(ex)}");
                return Result<User>.Error($"Error! : {ex.Message}");
            }
        }
    }
}
