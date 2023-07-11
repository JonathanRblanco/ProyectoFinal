using Ardalis.Result;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ProyectoFinal.Data;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.DTO.Responses;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.Handlers.UserHanlders
{
    public class EditUserHanlder : IRequestHandler<EditUserRequest, Result<EditUserResponse>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ModifyUserHandler> _logger;
        private readonly IMapper _mapper;
        private readonly RoleManager<Rol> _roleManager;

        public EditUserHanlder(UserManager<User> userManager,
            IUnitOfWork unitOfWork,
            ILogger<ModifyUserHandler> logger,
            IMapper mapper, RoleManager<Rol> roleManager)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _roleManager = roleManager;
        }
        public async Task<Result<EditUserResponse>> Handle(EditUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.UserId) || !Guid.TryParse(request.UserId, out var id))
                {
                    return Result.Error("El id no esta en un formato correcto");
                }
                var user = await _unitOfWork.UsersRepository.GetUserByIdAsync(id, _unitOfWork.Transaction);
                var response = _mapper.Map<EditUserResponse>(user);
                response.AllRoles = _roleManager.Roles.ToList();
                response.UsuRoles = await _userManager.GetRolesAsync(user);
                return Result.Success(response);
            }
            catch (Exception ex)
            {
                _unitOfWork.RollBack();
                _logger.LogError("Error : {0} {1}", ex.Message, ex.StackTrace);
                return Result.Error(ex.Message);
            }
        }
    }
}
