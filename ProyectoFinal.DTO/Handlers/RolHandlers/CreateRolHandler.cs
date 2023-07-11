using Ardalis.Result;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ProyectoFinal.Data;
using ProyectoFinal.DTO.Requests;

namespace ProyectoFinal.Handlers.RolHandlers
{
    public class CreateRolHandler : IRequestHandler<CreateRolRequest, Result>
    {
        private readonly RoleManager<Rol> _roleManager;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateRolHandler> _logger;

        public CreateRolHandler(RoleManager<Rol> roleManager,
            IMapper mapper,
            ILogger<CreateRolHandler> logger)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Result> Handle(CreateRolRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _roleManager.CreateAsync(_mapper.Map<Rol>(request));
                if (result.Succeeded)
                {
                    return Result.Success();
                }
                return Result.Error("No se ha podido crear el Rol," + string.Join(", ", result.Errors.Select(e => e.Description)) + ", intente nuevamente");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error : {0} {1}", ex.Message, ex.StackTrace);
                return Result.Error("No se ha podido crear el Rol, " + ex.Message + ", intente nuevamente");
            }
        }
    }
}
