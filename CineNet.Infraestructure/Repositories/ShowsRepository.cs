using CineNet.Domain.Contracts;
using CineNet.Domain.Entities;
using Dapper;
using System.Data;

namespace CineNet.Infraestructure.Repositories
{
    public class ShowsRepository : IShowsRepository
    {
        private readonly IDbConnection connection;

        public ShowsRepository(IDbConnection connection)
        {
            this.connection = connection;
        }

        public async Task<int> Create(Show show, IDbTransaction transaction)
        {
            return await connection.ExecuteScalarAsync<int>(createQuery, show, transaction);
        }

        public async Task Update(Show show, IDbTransaction transaction)
        {
            await connection.ExecuteAsync(updateQuery, show, transaction);
        }

        public async Task Delete(int id, IDbTransaction transaction)
        {
            await connection.ExecuteAsync(deleteQuery, new { Id = id }, transaction);
        }

        public async Task<IEnumerable<Show>> GetAllAsync(IDbTransaction transaction)
        {
            return await connection.QueryAsync<Show>(getAllQuery, transaction);
        }

        public async Task<Show> GetById(int id, IDbTransaction transaction)
        {
            return await connection.QueryFirstOrDefaultAsync<Show>(getByIdQuery, new { Id = id }, transaction);
        }

        private const string createQuery = @"INSERT INTO Shows (ShowTypeId,Date,MovieId,RoomId,Capacity,Price)
                                            VALUES(@ShowTypeId,@Date,@MovieId,@RoomId,@Capacity,@Price)
                                            SELECT CAST(SCOPE_IDENTITY() AS int)";
        private const string updateQuery = @"UPDATE Shows
                                            SET ShowTypeId = @ShowTypeId, Date = @Date,MovieId = @MovieId,RoomId = @RoomId,Capacity = @Capacity,Price = @Price
                                            WHERE Id = @Id";
        private const string deleteQuery = @"DELETE FROM Shows
                                            WHERE Id = @Id";
        private const string getAllQuery = @"SELECT * FROM Shows";
        private const string getByIdQuery = @"SELECT * FROM Shows
                                            WHERE Id = @Id";
    }
}
