using CineNet.Domain.Entities;
using System.Data;

namespace CineNet.Domain.Contracts
{
    public interface IGendersRepository
    {
        Task<int> Create(Gender gender, IDbTransaction transaction);
        Task Delete(int id, IDbTransaction transaction);
        Task<IEnumerable<Gender>> GetAllAsync(IDbTransaction transaction);
        Task<Gender> GetById(int id, IDbTransaction transaction);
        Task Update(Gender gender, IDbTransaction transaction);
    }
}
