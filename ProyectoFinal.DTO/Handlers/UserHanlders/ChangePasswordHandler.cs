using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProyectoFinal.Data;
using ProyectoFinal.DTO.Requests;

namespace ProyectoFinal.DTO.Handlers.UserHanlders
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordRequest, Result>
    {
        private readonly UserManager<User> userManager;

        public ChangePasswordHandler(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<Result> Handle(ChangePasswordRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.ConfirmPassword))
                {
                    return Result.Error("Ambos campos deben ser completados para seguir el proceso");
                }
                if (!string.Equals(request.Password, request.ConfirmPassword))
                {
                    return Result.Error("Ambos campos deben coincidir para seguir el proceso");
                }
                var user = await userManager.FindByIdAsync(request.UserId);
                if (user == null)
                {
                    return Result.Error("Usuario Invalido");
                }
                var isValidToken = await userManager.VerifyUserTokenAsync(user, userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", request.Token);
                if (!isValidToken)
                {
                    return Result.Error("Token invalido");
                }
                var result = await userManager.ResetPasswordAsync(user, request.Token, request.ConfirmPassword);
                if (result.Succeeded)
                {
                    return Result.Success();
                }
                return Result.Invalid(result.Errors.Select(e => new ValidationError() { ErrorCode = e.Code, ErrorMessage = e.Description }).ToList());
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message);
            }
        }
    }
}
