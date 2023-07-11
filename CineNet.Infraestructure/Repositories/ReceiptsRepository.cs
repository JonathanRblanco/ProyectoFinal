using CineNet.Domain.Contracts;
using CineNet.Domain.Entities;
using Dapper;
using System.Data;

namespace CineNet.Infraestructure.Repositories
{
    public class ReceiptsRepository : IReceiptsRepository
    {
        private readonly IDbConnection connection;

        public ReceiptsRepository(IDbConnection connection)
        {
            this.connection = connection;
        }
        public async Task<int> Create(Receipt receipt, IDbTransaction transaction)
        {
            return await connection.ExecuteScalarAsync<int>(createQuery, receipt, transaction);
        }

        public async Task<IEnumerable<Receipt>> GetAllAsync(IDbTransaction transaction)
        {
            return await connection.QueryAsync<Receipt>(getAllQuery, transaction);
        }

        public async Task<Receipt> GetById(int id, IDbTransaction transaction)
        {
            return await connection.QueryFirstOrDefaultAsync<Receipt>(getByIdQuery, new { Id = id }, transaction);
        }

        private const string createQuery = @"INSERT INTO Receipts
                                            VALUES(@ShowId,@UserId,@Total,@Date,@Code,@AmountOfTickets);
                                            SELECT CAST(SCOPE_IDENTITY() AS int)";
        private const string getAllQuery = @"SELECT * 
                                            FROM Receipts
                                            ORDER BY Id DESC";
        private const string getByIdQuery = @"SELECT * FROM Receipts
                                            WHERE Id = @Id";
    }
}
