using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.Movies
{
    public class CreateMoviesHandler : IRequestHandler<CreateMovieRequest, Result>
    {
        private readonly IMovieService movieService;

        public CreateMoviesHandler(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        public async Task<Result> Handle(CreateMovieRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var formdata = new MultipartFormDataContent
                {
                    { new StringContent(request.Name), "Name" },
                    { new StringContent(request.Duration), "Duration" },
                    { new StringContent(request.Actors), "Actors" },
                    { new StringContent(request.Director), "Director" },
                    { new StringContent(request.GenderId.ToString()), "GenderId" },
                    { new StringContent(request.ClasificationId.ToString()), "ClasificationId" },
                    {new StringContent(request.Synopsis), "Synopsis" }
                };

                var fileContent = new StreamContent(request.Image.OpenReadStream());
                formdata.Add(fileContent, "Image", request.Image.FileName);
                await movieService.CreateMovies(formdata);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Error($"Error! : {ex.Message}");
            };
        }
    }
}
