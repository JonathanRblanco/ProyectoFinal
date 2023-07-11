using CineNet.Domain.Entities;
using System.Data;

namespace CineNet.Domain.Contracts
{
    public interface IShowsRepository
    {
        Task<int> Create(Show show, IDbTransaction transaction);
        Task Delete(int id, IDbTransaction transaction);
        Task<IEnumerable<Show>> GetAllAsync(IDbTransaction transaction);
        Task<Show> GetById(int id, IDbTransaction transaction);
        Task Update(Show show, IDbTransaction transaction);
    }
}
