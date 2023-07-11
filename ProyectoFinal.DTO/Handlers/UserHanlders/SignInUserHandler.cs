using Ardalis.Result;
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
    public class SignInUserHandler : IRequestHandler<SignInUserRequest, Result>
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<SignInUserHandler> _logger;
        private readonly IEmailTasksService _emailTasksService;

        public SignInUserHandler(SignInManager<User> signInManager,
            UserManager<User> userManager,
            ILogger<SignInUserHandler> logger,
            IEmailTasksService emailTasksService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _emailTasksService = emailTasksService;
        }
        public async Task<Result> Handle(SignInUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(request.UserName);
                if (user is null)
                {
                    var errores = new List<ValidationError>
                {
                    new ValidationError() { ErrorMessage = "El usuario o la contraseña no coinciden" }
                };
                    return Result.Invalid(errores);
                }

                var checkPassword = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
                if (checkPassword.IsLockedOut)
                {
                    var errores = new List<ValidationError>
                    {
                    new ValidationError() { ErrorMessage = "Su cuenta se encuentra bloqueada"}
                    };
                    return Result.Invalid(errores);
                }
                if (checkPassword.IsNotAllowed)
                {
                    var emailTask = new EmailTask
                    {
                        Data = JsonSerializer.Serialize(new { UserId = user.Id }),
                        Type = "ConfirmEmail"
                    };
                    await _emailTasksService.CreateTask(JsonSerializer.Serialize(emailTask));
                    var errores = new List<ValidationError>
                    {
                    new ValidationError() { ErrorMessage = "Necesitas confirmar tu cuenta, te enviaremos un correo para que confirmes tu cuenta"}
                    };
                    return Result.Invalid(errores);
                }
                if (!checkPassword.Succeeded)
                {
                    await _userManager.AccessFailedAsync(user);
                    var errores = new List<ValidationError>
                    {
                    new ValidationError() { ErrorMessage = "Las credenciales proporcionadas no son validas" }
                    };
                    return Result.Invalid(errores);
                }
                await _signInManager.SignInAsync(user, request.RememberMe);

                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error : {0} {1}", ex.Message, ex.StackTrace);
                return Result.Error("No se ha podido iniciar sesion, intente nuevamente");
            }

        }
    }
}
