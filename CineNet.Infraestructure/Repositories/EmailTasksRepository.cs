using CineNet.Domain.Contracts;
using CineNet.Domain.Entities;
using Dapper;
using System.Data;

namespace CineNet.Infraestructure.Repositories
{
    public class EmailTasksRepository : IEmailTasksRepository
    {
        private readonly IDbConnection connection;

        public EmailTasksRepository(IDbConnection connection)
        {
            this.connection = connection;
        }
        public async Task<IEnumerable<EmailTask>> Get(IDbTransaction transaction)
        {
            return await connection.QueryAsync<EmailTask>(getAllQuery, transaction);
        }
        public async Task Create(EmailTask task, IDbTransaction transaction)
        {
            await connection.ExecuteScalarAsync<int>(createQuery, task, transaction);
        }

        public async Task Delete(int id, IDbTransaction transaction)
        {
            await connection.ExecuteAsync(deleteTaskQuery, new { Id = id }, transaction);
        }

        private const string createQuery = @"INSERT INTO EmailTasks
                                            VALUES(@Data,@Status,@Type)
                                            SELECT CAST(SCOPE_IDENTITY() AS int)";
        private const string getAllQuery = @"SELECT * 
                                            FROM EmailTasks
                                            Where Status <> 'Completada'";
        private const string deleteTaskQuery = @"UPDATE EmailTasks
                                                SET Status = 'Completada'
                                                WHERE Id = @Id";
    }
}
