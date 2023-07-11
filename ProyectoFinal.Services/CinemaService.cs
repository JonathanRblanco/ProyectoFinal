using ProyectoFinal.Data;
using ProyectoFinal.ServicesContracts;
using System.Text.Json;

namespace ProyectoFinal.Services
{
    public class CinemaService : ICinemaService
    {
        private readonly IApiService apiService;

        public CinemaService(IApiService apiService)
        {
            this.apiService = apiService;
        }
        public async Task<string> GetAll()
        {
            return await apiService.GetAsync("Cinemas");
        }
        public async Task<T> GetAll<T>()
        {
            var response = await this.GetAll();
            return JsonSerializer.Deserialize<T>(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
        public async Task<string> Create(string json)
        {
            return await apiService.PostJsonAsync("Cinemas", json);
        }
        public async Task<string> Update(string json)
        {
            return await apiService.PutJsonAsync("Cinemas", json);
        }
        public async Task<string> Delete(int id)
        {
            return await apiService.DeleteAsync("Cinemas/" + id);
        }
        public async Task<string> GetById(int id)
        {
            return await apiService.GetAsync("Cinemas/" + id);
        }
        public async Task<T> GetById<T>(int id)
        {
            var response = await this.GetById(id);
            return JsonSerializer.Deserialize<T>(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
        public async Task<string> GetCinemasByUserId(string userId)
        {
            return await apiService.GetAsync("Cinemas/user/" + userId + "/cinemas");
        }
        public async Task<T> GetCinemasByUserId<T>(string userId)
        {
            var response = await this.GetCinemasByUserId(userId);
            return JsonSerializer.Deserialize<T>(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }

        public async Task<string> ModifyAssignCinema(string json)
        {
            return await apiService.PostJsonAsync("Cinemas/assign", json);
        }

        public async Task<string> getStatistics(int cinemaId, int year, int? branchId)
        {
            var query = $"Cinemas/Statistics?CinemaId={cinemaId}&Year={year}";
            if (branchId.HasValue)
            {
                query = query + $"&Branch={branchId.Value}";
            }
            return await apiService.GetAsync(query);
        }

        public async Task<T> getStatistics<T>(int cinemaId, int year, int? branchId)
        {
            var response = await this.getStatistics(cinemaId,year,branchId);
            return JsonSerializer.Deserialize<T>(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
    }
}
