using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProyectoFinal.DTOs.Requests;
using ProyectoFinal.Models;

namespace ProyectoFinal.Handlers.UserHanlders
{
    public class SignInUserHandler : IRequestHandler<SignInUserRequest, Result>
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<SignInUserHandler> _logger;

        public SignInUserHandler(SignInManager<User> signInManager, UserManager<User> userManager, ILogger<SignInUserHandler> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
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

                var checkPassword = await _signInManager.CheckPasswordSignInAsync(user, request.Password,false);
                if (checkPassword.IsLockedOut)
                {
                    var errores = new List<ValidationError>
                    {
                    new ValidationError() { ErrorMessage = "Su cuenta se encuentra bloqueada"}
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
                await _signInManager.SignInAsync(user, true);

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
