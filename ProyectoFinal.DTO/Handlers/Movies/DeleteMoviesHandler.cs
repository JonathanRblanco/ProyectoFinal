using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.Movies
{

    public class DeleteMoviesHandler : IRequestHandler<DeleteMovieRequest, Result>
    {
        private readonly IMovieService movieService;

        public DeleteMoviesHandler(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        public async Task<Result> Handle(DeleteMovieRequest request, CancellationToken cancellationToken)
        {
            try
            {

                await movieService.Delete(request.id);
                return Result.Success();
            }
            catch (Exception ex)
            {

                return Result.Error($"Error! : {ex.Message}");
            }
        }
    }
}
