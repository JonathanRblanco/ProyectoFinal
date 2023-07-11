using Ardalis.Result;
using MediatR;
using Newtonsoft.Json;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.Cinemas
{
    public class ModifyCinemaHandler : IRequestHandler<ModifyCinemaRequest, Result>
    {
        private readonly ICinemaService _cinemaService;
        public ModifyCinemaHandler(ICinemaService cinemaService)
        {
            _cinemaService = cinemaService;
        }
        public async Task<Result> Handle(ModifyCinemaRequest request, CancellationToken cancellation)
        {
            try
            {
                var json = JsonConvert.SerializeObject(request);
                await _cinemaService.Update(json);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Error($"Error! : {ex.Message}");
            }
        }
    }
}
