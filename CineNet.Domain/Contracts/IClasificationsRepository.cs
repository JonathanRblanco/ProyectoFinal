using CineNet.Domain.Entities;
using System.Data;

namespace CineNet.Domain.Contracts
{
    public interface IClasificationsRepository
    {
        Task<int> Create(Clasification clasification, IDbTransaction transaction);
        Task Delete(int id, IDbTransaction transaction);
        Task<IEnumerable<Clasification>> GetAllAsync(IDbTransaction transaction);
        Task<Clasification> GetById(int id, IDbTransaction transaction);
        Task Update(Clasification clasification, IDbTransaction transaction);
    }
}
