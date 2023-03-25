using MediatR;
using Microsoft.AspNetCore.Identity;
using ProyectoFinal.DTOs.Commands.Requests;

namespace ProyectoFinal.Handlers.UserHanlders
{
    public class SignInUserCommandHandler : IRequestHandler<SignInUserCommandRequest, bool>
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public SignInUserCommandHandler(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }
        public async Task<bool> Handle(SignInUserCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _signInManager.PasswordSignInAsync(request.Nombre, request.Password, true, false);
            return result.Succeeded;
        }
    }
}
