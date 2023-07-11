using CineNet.Domain.Contracts;
using CineNet.Domain.Entities;
using Dapper;
using System.Data;

namespace CineNet.Infraestructure.Repositories
{
    public class ClasificationsRepository : IClasificationsRepository
    {
        private readonly IDbConnection _connection;

        public ClasificationsRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<IEnumerable<Clasification>> GetAllAsync(IDbTransaction transaction)
        {
            return await _connection.QueryAsync<Clasification>(getAllClasificationsQuery, transaction);
        }
        public async Task<int> Create(Clasification clasification, IDbTransaction transaction)
        {
            return await _connection.ExecuteScalarAsync<int>(createClasificationQuery, clasification, transaction);
        }

        public async Task<Clasification> GetById(int id, IDbTransaction transaction)
        {
            return await _connection.QueryFirstOrDefaultAsync<Clasification>(getClasificationByIdQuery, new { Id = id }, transaction);
        }

        public async Task Update(Clasification clasification, IDbTransaction transaction)
        {
            await _connection.ExecuteAsync(updateClasificationQuery, clasification, transaction);
        }

        public async Task Delete(int id, IDbTransaction transaction)
        {
            await _connection.ExecuteAsync(deleteClasificationQuery, new { Id = id }, transaction);
        }

        private const string getAllClasificationsQuery = @"SELECT * 
                                                            FROM Clasifications";
        private const string createClasificationQuery = @"INSERT INTO Clasifications
                                                        VALUES(@Description);
                                                        SELECT CAST(SCOPE_IDENTITY() AS int)";
        private const string getClasificationByIdQuery = @"SELECT * FROM Clasifications
                                                        WHERE Id = @Id";
        private const string updateClasificationQuery = @"UPDATE Clasifications
                                                        SET Description = @Description
                                                        WHERE Id = @Id";
        private const string deleteClasificationQuery = @"DELETE FROM Clasifications
                                                            WHERE Id = @Id";
    }
}
