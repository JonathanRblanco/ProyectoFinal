using CineNet.Domain.Entities;
using System.Data;

namespace CineNet.Domain.Contracts
{
    public interface IMoviesRepository
    {
        public Task<Movie> GetMovieByIdAsync(int Id, IDbTransaction transaction);
        Task<IEnumerable<Movie>> GetAllAsync(IDbTransaction transaction);
        public Task<int> CreateMovie(Movie movie, IDbTransaction transaction);

        public Task Delete(int id, IDbTransaction transaction);

        Task Update(Movie movie, IDbTransaction transaction);

    }
}
