using Dapper;
using ProyectoFinal.Data;
using ProyectoFinal.ServicesContracts;
using System.Data;

namespace CineNet.Infraestructure.Repositorys
{
    public class UserRolesRepository : IUserRolesRepository
    {
        private readonly IDbConnection _connection;

        public UserRolesRepository(IDbConnection Connection)
        {
            _connection = Connection;
        }
        public async Task InsertUsuRol(UserRoles userRoles, IDbTransaction transaction)
        {
            await _connection.ExecuteAsync(insertUsuRolQuery, userRoles, transaction);
        }
        private const string insertUsuRolQuery = @"INSERT INTO UserRoles (UserId, RoleId)
                                                   VALUES (@UserId, @RoleId)";
    }
}
