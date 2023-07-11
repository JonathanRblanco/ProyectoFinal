using Ardalis.Result;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ProyectoFinal.Data;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.DTO.Responses;

namespace ProyectoFinal.Handlers.RolHandlers
{
    public class EditRolHandler : IRequestHandler<EditRolRequest, Result<EditRolResponse>>
    {
        private readonly RoleManager<Rol> _roleManager;
        private readonly ILogger<EditRolHandler> _logger;
        private readonly IMapper _mapper;

        public EditRolHandler(RoleManager<Rol> roleManager,
            ILogger<EditRolHandler> logger,
            IMapper mapper)
        {
            _roleManager = roleManager;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Result<EditRolResponse>> Handle(EditRolRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var rol = await _roleManager.FindByIdAsync(request.Id);
                if (rol == null)
                {
                    return Result.Invalid(new List<ValidationError> { new ValidationError { ErrorMessage = "El rol no existe" } });
                }
                var response = _mapper.Map<EditRolResponse>(rol);
                return Result.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error : {0} {1}", ex.Message, ex.StackTrace);
                return Result.Error(ex.Message);
            }
        }
    }
}
