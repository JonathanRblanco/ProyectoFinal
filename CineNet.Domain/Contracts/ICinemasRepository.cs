using CineNet.Domain.Entities;
using System.Data;

namespace CineNet.Domain.Contracts
{
    public interface ICinemasRepository
    {
        public Task<IEnumerable<Cinema>> GetAllCines(IDbTransaction transaction);
        public Task<int> CreateCine(Cinema cine, IDbTransaction transaction);
        public Task Delete(int id, IDbTransaction transaction);
        public Task Update(Cinema cine, IDbTransaction transaction);
        Task<Cinema> GetById(int id, IDbTransaction transaction);
        public Task<IEnumerable<Cinema>> GetCinemasByUserId(string userId, IDbTransaction transaction);
        Task DeleteCinemasAssignByUserId(string userId, IDbTransaction transaction);
        Task InsertCinemasAssignByUserId(string userId, int cinemaId, IDbTransaction transaction);
        Task<IEnumerable<Statistics>> GetStatistics(int cinemaId, int year, int? branch, IDbTransaction transaction);
    }
}