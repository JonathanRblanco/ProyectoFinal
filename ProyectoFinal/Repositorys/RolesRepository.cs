using Dapper;
using ProyectoFinal.Contracts;
using System.Data;

namespace ProyectoFinal.Repositorys
{
    public class RolesRepository : IRolesRepository
    {
        private readonly IDbConnection _connection;

        public RolesRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<string> GetRoleByRoleName(string roleName, IDbTransaction transaction)
        {
            var roleId = await _connection.ExecuteScalarAsync<string>(getRoleByRoleName, new { NormalizedName = roleName }, transaction);
            return roleId;
        }
        private const string getRoleByRoleName = @"SELECT Id FROM Roles 
                                                    WHERE UPPER(NormalizedName) = UPPER(@NormalizedName)";
    }
}
