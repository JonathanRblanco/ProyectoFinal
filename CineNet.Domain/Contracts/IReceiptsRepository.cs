using CineNet.Domain.Entities;
using System.Data;

namespace CineNet.Domain.Contracts
{
    public interface IReceiptsRepository
    {
        Task<int> Create(Receipt receipt, IDbTransaction transaction);
        Task<IEnumerable<Receipt>> GetAllAsync(IDbTransaction transaction);
        Task<Receipt> GetById(int id, IDbTransaction transaction);
    }
}
