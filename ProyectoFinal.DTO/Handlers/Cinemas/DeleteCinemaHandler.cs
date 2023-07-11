using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.Cinemas
{
    public class DeleteCinemaHandler : IRequestHandler<DeleteCinemaRequest, Result>
    {
        private readonly ICinemaService _cinemaService;
        public DeleteCinemaHandler(ICinemaService cinemaService)
        {
            _cinemaService = cinemaService;
        }
        public async Task<Result> Handle(DeleteCinemaRequest request, CancellationToken cancellation)
        {
            try
            {
                await _cinemaService.Delete(request.Id);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Error($"Error! : {ex.Message}");
            }
        }
    }
}
