using ProyectoFinal.ServicesContracts;
using System.Text.Json;

namespace ProyectoFinal.Services
{
    public class ShowsService : IShowsService
    {
        private readonly IApiService apiService;

        public ShowsService(IApiService apiService)
        {
            this.apiService = apiService;
        }
        public async Task<string> Create(string data)
        {
            return await apiService.PostJsonAsync("Shows", data);
        }

        public async Task Delete(int id)
        {
            await apiService.DeleteAsync("Shows/" + id);
        }

        public async Task<string> GetAll()
        {
            return await apiService.GetAsync("Shows");
        }
        public async Task<T> GetAll<T>()
        {
            var response = await this.GetAll();
            return JsonSerializer.Deserialize<T>(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
        public async Task<string> GetById(int id)
        {
            return await apiService.GetAsync("Shows/" + id);
        }
        public async Task<T> GetById<T>(int id)
        {
            var response = await this.GetById(id);
            return JsonSerializer.Deserialize<T>(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }

        public async Task<string> Update(string json)
        {
            return await apiService.PutJsonAsync("Shows", json);
        }
    }
}
