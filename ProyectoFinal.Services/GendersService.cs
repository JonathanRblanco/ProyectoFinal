using ProyectoFinal.ServicesContracts;
using System.Text.Json;

namespace ProyectoFinal.Services
{
    public class GendersService : IGendersService
    {
        private readonly IApiService apiService;

        public GendersService(IApiService apiService)
        {
            this.apiService = apiService;
        }
        public async Task<string> GetAll()
        {
            return await apiService.GetAsync("Genders");
        }
        public async Task<T> GetAll<T>()
        {
            var response = await this.GetAll();
            return JsonSerializer.Deserialize<T>(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
        public async Task<string> Create(string json)
        {
            return await apiService.PostJsonAsync("Genders", json);
        }
        public async Task<string> Update(string json)
        {
            return await apiService.PutJsonAsync("Genders", json);
        }
        public async Task<string> Delete(int id)
        {
            return await apiService.DeleteAsync("Genders/" + id);
        }
        public async Task<string> GetById(int id)
        {
            return await apiService.GetAsync("Genders/" + id);
        }
        public async Task<T> GetById<T>(int id)
        {
            var response = await this.GetById(id);
            return JsonSerializer.Deserialize<T>(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
    }
}
