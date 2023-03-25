using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProyectoFinal.DTOs.Commands.Requests;
using System.Text.Json;


namespace ProyectoFinal.Handlers.UserHanlders
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, IdentityUser>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateUserCommandHandler> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateUserCommandHandler(IMapper mapper, ILogger<CreateUserCommandHandler> logger, UserManager<IdentityUser> userManager)
        {
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IdentityUser> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            var userLogin = new IdentityUser();
            try
            {
                _logger.LogInformation($"Consultando repositorio: Request:{JsonSerializer.Serialize(request)}");
                var user = _mapper.Map<IdentityUser>(request);
                var result = await _userManager.CreateAsync(user, request.Pass);
                if (result.Succeeded)
                {
                    userLogin = user;
                    _logger.LogInformation($"Respuesta Handler:{JsonSerializer.Serialize(userLogin)}");
                    return userLogin;
                }
                _logger.LogInformation($"Respuesta Handler:{JsonSerializer.Serialize(userLogin)}");
                return userLogin;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error! : {0} {1}", ex.Message, ex.StackTrace);
                _logger.LogInformation($"Respuesta Handler:{JsonSerializer.Serialize(userLogin)}");
                return userLogin;
            }
        }
    }
}
