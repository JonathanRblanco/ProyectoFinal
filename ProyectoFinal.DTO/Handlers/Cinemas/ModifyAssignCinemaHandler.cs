using Ardalis.Result;
using MediatR;
using Newtonsoft.Json;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.Cinemas
{
    public class ModifyAssignCinemaHandler : IRequestHandler<ModifyAssignCinemaRequest, Result>
    {
        private readonly ICinemaService _cinemaService;
        public ModifyAssignCinemaHandler(ICinemaService cinemaService)
        {
            _cinemaService = cinemaService;
        }
        public async Task<Result> Handle(ModifyAssignCinemaRequest request, CancellationToken cancellation)
        {
            try
            {
                var json = JsonConvert.SerializeObject(request);
                await _cinemaService.ModifyAssignCinema(json);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Error($"Error! : {ex.Message}");
            }
        }
    }
}
