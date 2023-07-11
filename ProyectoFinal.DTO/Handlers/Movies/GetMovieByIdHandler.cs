using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.DTO.Responses;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.Movies
{
    public class GetMovieByIdHandler : IRequestHandler<GetMovieByIdRequest, Result<GetMovieByIdResponse>>
    {
        private readonly IMovieService movieService;

        public GetMovieByIdHandler(IMovieService movieService)
        {
            this.movieService = movieService;
        }
        public async Task<Result<GetMovieByIdResponse>> Handle(GetMovieByIdRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var movie = await movieService.GetMovieById<GetMovieByIdResponse>(request.Id);
                if (movie == null)
                {
                    return Result.Error("La pelicula no existe");
                }
                return Result.Success(movie);
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message);
            }
        }
    }
}

