using CineNet.Domain.Entities;
using System.Data;

namespace CineNet.Domain.Contracts
{
    public interface IShowsTypeRepository
    {
        Task<int> Create(ShowType showType, IDbTransaction transaction);
        Task Delete(int id, IDbTransaction transaction);
        Task<IEnumerable<ShowType>> GetAllAsync(IDbTransaction transaction);
        Task<ShowType> GetById(int id, IDbTransaction transaction);
        Task Update(ShowType showType, IDbTransaction transaction);
    }
}
