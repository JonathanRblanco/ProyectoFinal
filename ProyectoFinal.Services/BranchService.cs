using ProyectoFinal.ServicesContracts;
using System.Text.Json;

namespace ProyectoFinal.Services
{
    public class BranchService : IBranchService
    {
        private readonly IApiService apiService;

        public BranchService(IApiService apiService)
        {
            this.apiService = apiService;
        }

        public async Task<string> GetAll()
        {
            return await apiService.GetAsync("Branch");
        }

        public async Task<T> GetAll<T>()
        {
            var response = await this.GetAll();
            return JsonSerializer.Deserialize<T>(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }

        public async Task<string> Create(string data)
        {
            return await apiService.PostJsonAsync("Branch", data);
        }
        public async Task<string> GetById(int id)
        {
            return await apiService.GetAsync("Branch/" + id);
        }
        public async Task<T> GetById<T>(int id)
        {
            var response = await this.GetById(id);
            return JsonSerializer.Deserialize<T>(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
        public async Task<string> Update(string json)
        {
            return await apiService.PutJsonAsync("Branch", json);
        }
        public async Task Delete(int id)
        {
            await apiService.DeleteAsync("Branch/" + id);
        }
    }
}
