using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.DTO.Responses;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.Cinemas
{
    public class GetCinemaByIdHandler : IRequestHandler<GetCinemaByIdRequest, Result<GetCinemaByIdResponse>>
    {
        private readonly ICinemaService cinemaService;

        public GetCinemaByIdHandler(ICinemaService cinemaService)
        {
            this.cinemaService = cinemaService;
        }
        public async Task<Result<GetCinemaByIdResponse>> Handle(GetCinemaByIdRequest request, CancellationToken cancellation)
        {
            try
            {
                var cinema = await cinemaService.GetById<GetCinemaByIdResponse>(request.Id);
                if (cinema == null)
                {
                    return Result.Error("El cine no existe");
                }
                return Result.Success(cinema);
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message);
            }
        }
    }
}
