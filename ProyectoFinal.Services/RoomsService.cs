using ProyectoFinal.ServicesContracts;
using System.Text.Json;

namespace ProyectoFinal.Services
{
    public class RoomsService : IRoomsService
    {
        private readonly IApiService _apiService;

        public RoomsService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<string> Create(string data)
        {
            return await _apiService.PostJsonAsync("Rooms", data);
        }

        public async Task Delete(int id)
        {
            await _apiService.DeleteAsync("Rooms/" + id);
        }

        public async Task<string> GetAll()
        {
            return await _apiService.GetAsync("Rooms");
        }
        public async Task<T> GetAll<T>()
        {
            var response = await this.GetAll();
            return JsonSerializer.Deserialize<T>(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
        public async Task<string> GetById(int id)
        {
            return await _apiService.GetAsync("Rooms/" + id);
        }
        public async Task<T> GetById<T>(int id)
        {
            var response = await this.GetById(id);
            return JsonSerializer.Deserialize<T>(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }

        public async Task<string> Update(string json)
        {
            return await _apiService.PutJsonAsync("Rooms", json);
        }
    }
}
