using Microsoft.AspNetCore.Http;

namespace ProyectoFinal.ServicesContracts
{
    public interface IImageService
    {
        Task<string> Create(IFormFile file);
        Task<T> Create<T>(IFormFile file);
        Task<string> GetById(int id);
        Task<T> GetById<T>(int id);
    }
}
