using Microsoft.AspNetCore.Http;
using ProyectoFinal.ServicesContracts;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ProyectoFinal.Services
{
    public class ImageService : IImageService
    {
        private readonly IApiService apiService;
        private readonly HttpClient client;

        public ImageService(IApiService apiService, HttpClient client)
        {
            this.apiService = apiService;
            this.client = client;
        }

        public async Task<string> GetById(int id)
        {
            return await apiService.GetAsync($"Images/{id}");
        }
        public async Task<T> GetById<T>(int id)
        {
            var response = await this.GetById(id);
            return JsonSerializer.Deserialize<T>(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
        public async Task<string> Create(IFormFile file)
        {
            var requestContent = new MultipartFormDataContent();
            var fileContent = new StreamContent(file.OpenReadStream());
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(file.ContentType);
            requestContent.Add(fileContent, "file", file.FileName);
            return await apiService.PostAsync("Images", requestContent);
        }
        public async Task<T> Create<T>(IFormFile file)
        {
            var response = await Create(file);
            return JsonSerializer.Deserialize<T>(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
    }
}
