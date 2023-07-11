using Ardalis.Result;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ProyectoFinal.Data;
using ProyectoFinal.DTO.Requests;

namespace ProyectoFinal.Handlers.RolHandlers
{
    public class DeleteRolHanlder : IRequestHandler<DeleteRolRequest, Result>
    {
        private readonly RoleManager<Rol> _roleManager;
        private readonly ILogger<DeleteRolHanlder> _logger;
        private readonly IMapper _mapper;

        public DeleteRolHanlder(RoleManager<Rol> roleManager,
            ILogger<DeleteRolHanlder> logger,
            IMapper mapper)
        {
            _roleManager = roleManager;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Result> Handle(DeleteRolRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _roleManager.DeleteAsync(_mapper.Map<Rol>(request));
                if (result.Succeeded)
                {
                    return Result.Success();
                }
                return Result.Error("No se ha podido eliminar el Rol, " + string.Join(", ", result.Errors.Select(e => e.Description)) + ", intente nuevamente");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error : {0} {1}", ex.Message, ex.StackTrace);
                return Result.Error("No se ha podido editar el Rol, " + ex.Message + ", intente nuevamente");
            }
        }
    }
}
