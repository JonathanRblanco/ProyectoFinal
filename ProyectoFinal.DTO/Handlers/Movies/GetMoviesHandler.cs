using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.DTO.Responses;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.Movies
{
    public class GetMoviesHandler : IRequestHandler<GetMoviesRequest, Result<IEnumerable<GetMoviesResponse>>>
    {
        private readonly IMovieService movieService;
        private readonly IShowsService showsService;

        public GetMoviesHandler(IMovieService movieService,
            IShowsService showsService)
        {
            this.movieService = movieService;
            this.showsService = showsService;
        }
        public async Task<Result<IEnumerable<GetMoviesResponse>>> Handle(GetMoviesRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var movies = await movieService.GetAll<IEnumerable<GetMoviesResponse>>();
                if (request.OnlyWithShows)
                {
                    var shows = await showsService.GetAll<IEnumerable<GetShowsResponse>>();
                    movies = movies.Where(m => shows.Any(s => s.MovieId == m.Id && s.Capacity > 0 && s.Date.DateTime > DateTime.UtcNow)).ToList();
                }
                if (!string.IsNullOrEmpty(request.Name) && !string.IsNullOrWhiteSpace(request.Name))
                {
                    movies = movies.Where(m => m.Name.ToUpper().Contains(request.Name.ToUpper())).ToList();
                }
                if (request.Id > 0)
                {
                    movies = movies.Where(m => m.Id == request.Id).ToList();
                }
                return Result.Success(movies);
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message);
            }
        }
    }
}
