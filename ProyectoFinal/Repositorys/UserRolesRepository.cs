using Dapper;
using ProyectoFinal.Contracts;
using System.Data;

namespace ProyectoFinal.Repositorys
{
    //TODO IMPLEMENTAR LOS METODOS NECESARIOS DEL REPOSITORIO
    public class UserRolesRepository : IUserRolesRepository
    {
        private readonly IDbConnection _connection;

        public UserRolesRepository(IDbConnection Connection)
        {
            _connection = Connection;
        }
        public async Task InsertUsuRol(string roleId, string usuId, IDbTransaction transaction)
        {
            await _connection.ExecuteAsync(insertUsuRolQuery, new { UserId = usuId, RoleId = roleId }, transaction);
        }
        private const string insertUsuRolQuery = @"INSERT INTO UserRoles (UserId, RoleId)
                                                   VALUES (@UserId, @RoleId)";
    }
}
