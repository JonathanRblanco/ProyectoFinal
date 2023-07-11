using ProyectoFinal.ServicesContracts;
using System.Text.Json;

namespace ProyectoFinal.Services
{
    public class ReviewsService : IReviewsService
    {
        private readonly IApiService apiService;

        public ReviewsService(IApiService apiService)
        {
            this.apiService = apiService;
        }
        public async Task<string> GetAll()
        {
            return await apiService.GetAsync("Reviews");
        }
        public async Task<T> GetAll<T>()
        {
            var response = await this.GetAll();
            return JsonSerializer.Deserialize<T>(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
        public async Task<string> Create(string json)
        {
            return await apiService.PostJsonAsync("Reviews", json);
        }
    }
}
