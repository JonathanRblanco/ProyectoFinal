using Ardalis.Result;
using MediatR;
using Newtonsoft.Json;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.Cinemas
{
    public class CreateCinemaHandler : IRequestHandler<CreateCinemaRequest, Result>
    {
        private readonly ICinemaService cinemaService;

        public CreateCinemaHandler(ICinemaService cinemaService)
        {
            this.cinemaService = cinemaService;
        }
        public async Task<Result> Handle(CreateCinemaRequest request, CancellationToken cancellation)
        {
            try
            {
                var json = JsonConvert.SerializeObject(request);
                await cinemaService.Create(json);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Error($"Error! : {ex.Message}");
            }
        }
    }
}
