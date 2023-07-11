using CineNet.Domain.Contracts;
using CineNet.Domain.Entities;
using Dapper;
using System.Data;

namespace CineNet.Infraestructure.Repositories
{
    public class GendersRepository : IGendersRepository
    {
        private readonly IDbConnection _connection;

        public GendersRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<IEnumerable<Gender>> GetAllAsync(IDbTransaction transaction)
        {
            return await _connection.QueryAsync<Gender>(getAllGendersQuery, transaction);
        }
        public async Task<int> Create(Gender gender, IDbTransaction transaction)
        {
            return await _connection.ExecuteScalarAsync<int>(createGenderQuery, gender, transaction);
        }
        public async Task Update(Gender gender, IDbTransaction transaction)
        {
            await _connection.ExecuteAsync(updateGenderQuery, gender, transaction);
        }
        public async Task<Gender> GetById(int id, IDbTransaction transaction)
        {
            return await _connection.QueryFirstOrDefaultAsync<Gender>(getGenderByIdQuery, new { Id = id }, transaction);
        }

        public async Task Delete(int id, IDbTransaction transaction)
        {
            await _connection.ExecuteAsync(deleteGenderQuery, new { Id = id }, transaction);
        }

        private const string getAllGendersQuery = @"SELECT * FROM Genders";
        private const string createGenderQuery = @"INSERT INTO Genders
                                                    VALUES(@Description); 
                                                    SELECT CAST(SCOPE_IDENTITY() AS int)";
        private const string getGenderByIdQuery = @"SELECT * FROM Genders
                                                    WHERE Id = @Id";
        private const string updateGenderQuery = @"UPDATE Genders
                                                   SET Description = @Description
                                                   WHERE Id = @Id";
        private const string deleteGenderQuery = @"DELETE FROM Genders
                                                   WHERE Id = @Id";
    }
}
