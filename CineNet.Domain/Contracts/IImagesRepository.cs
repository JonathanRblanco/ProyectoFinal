using CineNet.Domain.Entities;
using System.Data;

namespace CineNet.Domain.Contracts
{
    public interface IImagesRepository
    {
        Task<int> Create(byte[] file, IDbTransaction transaction);
        Task<Image> GetById(int id, IDbTransaction transaction);
        Task Update(int imageId, byte[] imageData, IDbTransaction transaction);
    }
}
