using ProyectoFinal.ServicesContracts;
using System.Text.Json;

namespace ProyectoFinal.Services
{
    public class ShowsTypeService : IShowsTypeService
    {
        private readonly IApiService apiService;

        public ShowsTypeService(IApiService apiService)
        {
            this.apiService = apiService;
        }
        public async Task<string> GetAll()
        {
            return await apiService.GetAsync("ShowsType");
        }
        public async Task<T> GetAll<T>()
        {
            var response = await this.GetAll();
            return JsonSerializer.Deserialize<T>(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
        public async Task<string> Create(string json)
        {
            return await apiService.PostJsonAsync("ShowsType", json);
        }
        public async Task<string> Update(string json)
        {
            return await apiService.PutJsonAsync("ShowsType", json);
        }
        public async Task<string> Delete(int id)
        {
            return await apiService.DeleteAsync("ShowsType/" + id);
        }
        public async Task<string> GetById(int id)
        {
            return await apiService.GetAsync("ShowsType/" + id);
        }
        public async Task<T> GetById<T>(int id)
        {
            var response = await this.GetById(id);
            return JsonSerializer.Deserialize<T>(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
    }
}
