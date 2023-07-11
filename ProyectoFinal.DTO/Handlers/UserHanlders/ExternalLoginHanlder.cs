using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ProyectoFinal.Data;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.ServicesContracts;
using System.Security.Claims;

namespace ProyectoFinal.DTO.Handlers.UserHanlders
{
    public class ExternalLoginHanlder : IRequestHandler<ExternalLoginRequest, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ExternalLoginHanlder(IUnitOfWork unitOfWork,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Result> Handle(ExternalLoginRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.RemoteError != null)
                {
                    return Result.Error($"Ha ocurrido un error desde el proveedor: {request.RemoteError}");
                }
                var result = await _httpContextAccessor.HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
                if (!result.Succeeded)
                {
                    return Result.Error($"Ha ocurrido un error al intentar autenticarse con el proveedor");
                }
                var user = await _userManager.FindByLoginAsync("Google", result.Principal.FindFirst(ClaimTypes.NameIdentifier).Value);
                if (user is not null)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return Result.Success();
                }
                _unitOfWork.BeginTransaction();
                user = new User
                {
                    UserName = result.Principal.FindFirst(ClaimTypes.Name).Value,
                    Email = result.Principal.FindFirst(ClaimTypes.Email).Value,
                    EmailConfirmed = true
                };

                var createResult = await _userManager.CreateAsync(user, "OAuth57" + user.Email);
                if (createResult.Succeeded)
                {
                    await _userManager.AddLoginAsync(user, new UserLoginInfo("Google", result.Principal.FindFirst(ClaimTypes.NameIdentifier).Value, "Google"));
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _unitOfWork.CommitTransaction();
                    return Result.Success();
                }
                else
                {
                    return Result.Invalid(createResult.Errors.Select(e => new ValidationError() { ErrorCode = e.Code, ErrorMessage = e.Description }).ToList());
                }
            }
            catch (Exception ex)
            {
                _unitOfWork.RollBack();
                return Result.Error(ex.Message);
            }

        }
    }
}
