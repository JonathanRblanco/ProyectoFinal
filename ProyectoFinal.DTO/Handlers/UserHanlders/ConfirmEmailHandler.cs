using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProyectoFinal.Data;
using ProyectoFinal.DTO.Requests;

namespace ProyectoFinal.DTO.Handlers.UserHanlders
{
    public class ConfirmEmailHandler : IRequestHandler<ConfirmEmailRequest, Result>
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public ConfirmEmailHandler(UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public async Task<Result> Handle(ConfirmEmailRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await userManager.FindByIdAsync(request.UserId);
                if (user == null)
                {
                    return Result.Error("No existe usuario con el Id: " + request.UserId);
                }
                var confirmResult = await userManager.ConfirmEmailAsync(user, request.Token);
                if (confirmResult.Succeeded)
                {
                    return Result.Success();
                }
                return Result.Invalid(confirmResult.Errors.Select(e => new ValidationError() { ErrorCode = e.Code, ErrorMessage = e.Description }).ToList());
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message);
            }
        }
    }
}
