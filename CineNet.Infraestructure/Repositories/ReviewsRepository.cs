using CineNet.Domain.Contracts;
using CineNet.Domain.Entities;
using Dapper;
using System.Data;

namespace CineNet.Infraestructure.Repositories
{
    public class ReviewsRepository : IReviewsRepository
    {
        private readonly IDbConnection _connection;
        public ReviewsRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<IEnumerable<Review>> GetAllAsync(IDbTransaction transaction)
        {
            return await _connection.QueryAsync<Review>(getAllReviewsQuery, transaction);
        }

        public async Task<int> Create(Review review, IDbTransaction transaction)
        {
            return await _connection.ExecuteScalarAsync<int>(createReviewQuery, review, transaction);
        }
        private const string createReviewQuery = @"INSERT INTO [dbo].[Reviews] ([Description], [UserId], [MovieId], [Date])
                                                    VALUES (@Description, @UserId, @MovieId, @Date);
                                                    SELECT CAST(SCOPE_IDENTITY() AS int)";

        private const string getAllReviewsQuery = @"SELECT * 
                                                    From Reviews
                                                    ORDER BY Date DESC";
    }
}
