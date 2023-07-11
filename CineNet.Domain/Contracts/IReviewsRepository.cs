using CineNet.Domain.Entities;
using System.Data;

namespace CineNet.Domain.Contracts
{
    public interface IReviewsRepository
    {
        Task<IEnumerable<Review>> GetAllAsync(IDbTransaction transaction);
        public Task<int> Create(Review review, IDbTransaction transaction);
    }
}
