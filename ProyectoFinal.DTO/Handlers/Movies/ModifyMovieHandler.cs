using Ardalis.Result;
using MediatR;
using Newtonsoft.Json;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.Movies
{
    public class ModifyMovieHandler : IRequestHandler<ModifyMovieRequest, Result>
    {
        private readonly IMovieService movieService;

        public ModifyMovieHandler(IMovieService movieService)
        {
            this.movieService = movieService;
        }
        public async Task<Result> Handle(ModifyMovieRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var jsonData = JsonConvert.SerializeObject(request);
                await movieService.Update(jsonData);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Error($"Error! : {ex.Message}");
            };
        }
    }
}
