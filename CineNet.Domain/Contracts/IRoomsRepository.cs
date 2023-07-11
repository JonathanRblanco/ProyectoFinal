using CineNet.Domain.Entities;
using System.Data;

namespace CineNet.Domain.Contracts
{
    public interface IRoomsRepository
    {
        Task<int> Create(Room room, IDbTransaction transaction);
        Task Delete(int id, IDbTransaction transaction);
        Task<IEnumerable<Room>> GetAllAsync(IDbTransaction transaction);
        Task<Room> GetById(int id, IDbTransaction transaction);
        Task Update(Room room, IDbTransaction transaction);
    }
}
