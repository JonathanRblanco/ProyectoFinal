using CineNet.Domain.Contracts;
using CineNet.Domain.Entities;
using Dapper;
using System.Data;

namespace CineNet.Infraestructure.Repositories
{
    public class ShowsTypeRepository : IShowsTypeRepository
    {
        private readonly IDbConnection connection;

        public ShowsTypeRepository(IDbConnection connection)
        {
            this.connection = connection;
        }

        public async Task<IEnumerable<ShowType>> GetAllAsync(IDbTransaction transaction)
        {
            return await connection.QueryAsync<ShowType>(getAllQuery, transaction);
        }

        public async Task<ShowType> GetById(int id, IDbTransaction transaction)
        {
            return await connection.QueryFirstOrDefaultAsync<ShowType>(getByIdQuery, new { Id = id }, transaction);
        }

        public async Task<int> Create(ShowType showType, IDbTransaction transaction)
        {
            return await connection.ExecuteScalarAsync<int>(createQuery, showType, transaction);
        }

        public async Task Update(ShowType showType, IDbTransaction transaction)
        {
            await connection.ExecuteAsync(updateQuery, showType, transaction);
        }

        public async Task Delete(int id, IDbTransaction transaction)
        {
            await connection.ExecuteAsync(deleteQuery, new { Id = id }, transaction);
        }

        private const string getAllQuery = @"SELECT * FROM
                                            ShowsType";
        private const string getByIdQuery = @"SELECT * FROM ShowsType
                                               WHERE Id = @Id";
        private const string createQuery = @"INSERT INTO ShowsType
                                            VALUES (@Description);
                                            SELECT CAST(SCOPE_IDENTITY() AS int)";
        private const string updateQuery = @"UPDATE ShowsType
                                            SET Description = @Description
                                            WHERE Id = @Id";
        private const string deleteQuery = @"DELETE FROM ShowsType
                                            WHERE Id = @Id";
    }
}
