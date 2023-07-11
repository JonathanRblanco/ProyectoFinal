using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ProyectoFinal.Data;
using ProyectoFinal.DTO.Requests;

namespace ProyectoFinal.Handlers.UserHanlders
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserRequest, Result>
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<ModifyUserHandler> _logger;

        public DeleteUserHandler(UserManager<User> userManager,
            ILogger<ModifyUserHandler> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }
        public async Task<Result> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId);
                if (user == null)
                {
                    return Result.Error();
                }
                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    return Result.Error();
                }
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error : {0} {1}", ex.Message, ex.StackTrace);
                return Result.Error();
            }
        }
    }
}
