using ProyectoFinal.ServicesContracts;
using System.Text.Json;

namespace ProyectoFinal.Services
{
    public class CheckOutService : ICheckOutService
    {
        private readonly IApiService apiService;

        public CheckOutService(IApiService apiService)
        {
            this.apiService = apiService;
        }
        public async Task<string> GetAll()
        {
            return await apiService.GetAsync("Receipts");
        }

        public async Task<T> GetAll<T>()
        {
            var response = await this.GetAll();
            return JsonSerializer.Deserialize<T>(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
        public async Task<string> Create(string data)
        {
            return await apiService.PostJsonAsync("Receipts", data);
        }

        public async Task<T> Create<T>(string data)
        {
            var response = await this.Create(data);
            return JsonSerializer.Deserialize<T>(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }

        public async Task<string> GetById(int id)
        {
            return await apiService.GetAsync("Receipts/" + id);
        }
        public async Task<T> GetById<T>(int id)
        {
            var response = await this.GetById(id);
            return JsonSerializer.Deserialize<T>(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
    }
}
