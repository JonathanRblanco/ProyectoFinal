using Ardalis.Result;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProyectoFinal.DTOs.Requests;
using ProyectoFinal.Models;
using System.Text.Json;


namespace ProyectoFinal.Handlers.UserHanlders
{
    public class CreateUserHandler : IRequestHandler<CreateUserRequest, Result<User>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateUserHandler> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public CreateUserHandler(IMapper mapper, ILogger<CreateUserHandler> logger, UserManager<User> userManager,SignInManager<User> signInManager)
        {
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<Result<User>> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Consultando repositorio: Request:{JsonSerializer.Serialize(request)}");
                var user = _mapper.Map<User>(request);
                var result = await _userManager.CreateAsync(user, request.Pass);
                if (result.Succeeded)
                {
                    _logger.LogInformation($"Respuesta Handler:{JsonSerializer.Serialize(user)}");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return Result<User>.Success(user);
                }
                _logger.LogInformation($"Respuesta Handler:{JsonSerializer.Serialize(user)}");
                return Result<User>.Invalid(result.Errors.Select(e => new ValidationError() { ErrorCode = e.Code, ErrorMessage = e.Description }).ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError("Error! : {0} {1}", ex.Message, ex.StackTrace);
                _logger.LogInformation($"Respuesta Handler:{JsonSerializer.Serialize(ex)}");
                return Result<User>.Error($"Error! : {ex.Message}");
            }
        }
    }
}
