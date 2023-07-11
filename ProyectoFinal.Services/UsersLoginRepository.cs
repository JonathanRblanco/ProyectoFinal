using Dapper;
using ProyectoFinal.Data;
using ProyectoFinal.ServicesContracts;
using System.Data;

namespace ProyectoFinal.Services
{
    public class UsersLoginRepository : IUserLoginsRepository
    {
        private readonly IDbConnection _connection;

        public UsersLoginRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<IEnumerable<UserLogin>> GetLoginsByUserId(string userId, IDbTransaction transaction)
        {
            return await _connection.QueryAsync<UserLogin>(getLoginsByUserIdQuery, new { UserId = userId }, transaction);
        }
        public async Task AddLogin(string userId, string loginProvider, string loginKey, string providerName, IDbTransaction transaction)
        {
            await _connection.ExecuteAsync(insertLoginQuery, new { loginProvider = loginProvider, providerKey = loginKey, providerName = providerName, userId = userId }, transaction);
        }
        public async Task RemoveLogin(string userId, string loginProvider, IDbTransaction transaction)
        {
            await _connection.ExecuteAsync(deleteLoginQuery, new { userId = userId, loginProvider = loginProvider }, transaction);
        }
        private const string getLoginsByUserIdQuery = @"SELECT * FROM UserLogins
                                                        WHERE UserId = @UserId";
        private const string insertLoginQuery = @"INSERT INTO UserLogins
                                                   ([LoginProvider]
                                                   ,[ProviderKey]
                                                   ,[ProviderDisplayName]
                                                   ,[UserId])
                                                    VALUES
                                                   (@loginProvider
                                                   ,@providerKey
                                                   ,@providerName
                                                   ,@userId)";
        private const string deleteLoginQuery = @"DELETE FROM UserLogins
                                                WHERE LoginProvider = @loginProvider
                                                AND UserId = @userId";
    }
}
