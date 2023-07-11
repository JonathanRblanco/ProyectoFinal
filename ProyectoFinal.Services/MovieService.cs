using ProyectoFinal.ServicesContracts;
using System.Text.Json;

namespace ProyectoFinal.Services
{
    public class MovieService : IMovieService
    {
        private readonly IApiService apiService;

        public MovieService(IApiService apiService)
        {
            this.apiService = apiService;
        }

        public async Task<string> GetAll()
        {
            return await apiService.GetAsync("Movies");
        }
        public async Task<T> GetAll<T>()
        {
            var response = await this.GetAll();
            return JsonSerializer.Deserialize<T>(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
        public async Task<string> CreateMovies(MultipartFormDataContent content)
        {
            return await apiService.PostAsync("Movies", content);
        }

        public async Task<string> Delete(int id)
        {
            return await apiService.DeleteAsync("Movies/" + id);
        }

        public async Task<string> GetMovieById(int id)
        {
            return await apiService.GetAsync("Movies/" + id);
        }

        public async Task<string> Update(string data)
        {
            return await apiService.PutJsonAsync("Movies", data);
        }

        public async Task<T> GetMovieById<T>(int id)
        {
            var response = await this.GetMovieById(id);
            return JsonSerializer.Deserialize<T>(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
    }
}
