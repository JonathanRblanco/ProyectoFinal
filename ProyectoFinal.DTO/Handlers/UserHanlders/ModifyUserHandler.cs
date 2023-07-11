using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ProyectoFinal.Data;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.Handlers.UserHanlders
{
    public class ModifyUserHandler : IRequestHandler<ModifyUserRequest, Result>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ModifyUserHandler> _logger;

        public ModifyUserHandler(UserManager<User> userManager,
                                 IUnitOfWork unitOfWork,
                                 ILogger<ModifyUserHandler> logger)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<Result> Handle(ModifyUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(request.Id);
                if (user == null)
                {
                    return Result.Error();
                }
                _unitOfWork.BeginTransaction();
                user.Email = request.Email;
                user.UserName = request.UserName;
                user.PhoneNumber = request.PhoneNumber;
                user.LockoutEnd = request.LockoutEnd;
                user.LockoutEnabled = request.LockoutEnd.HasValue ? true : false;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    if (request.OldRoles != null)
                    {
                        await _userManager.RemoveFromRolesAsync(user, request.OldRoles);
                    }
                    if (request.NewRoles != null)
                    {
                        await _userManager.AddToRolesAsync(user, request.NewRoles);
                    }

                    _unitOfWork.CommitTransaction();
                    return Result.Success();
                }
                return Result.Invalid(result.Errors.Select(e => new ValidationError() { ErrorCode = e.Code, ErrorMessage = e.Description }).ToList());
            }
            catch (Exception ex)
            {
                _unitOfWork.RollBack();
                _logger.LogError("Error : {0} {1}", ex.Message, ex.StackTrace);
                return Result.Error();
            }
        }
    }
}
