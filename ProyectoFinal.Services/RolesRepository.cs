using Dapper;
using ProyectoFinal.Data;
using ProyectoFinal.ServicesContracts;
using System.Data;

namespace ProyectoFinal.Services
{
    public class RolesRepository : IRolesRepository
    {
        private readonly IDbConnection _connection;

        public RolesRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<Rol> GetRoleByName(string roleName, IDbTransaction transaction)
        {
            return await _connection.QueryFirstOrDefaultAsync<Rol>(getRoleByRoleNameQuery, new { NormalizedName = roleName }, transaction);
        }

        public IQueryable<Rol> GetAll(IDbTransaction transaction)
        {
            return _connection.Query<Rol>(getAllQuery).AsQueryable();
        }

        public async Task Create(Rol role, IDbTransaction transaction)
        {
            await _connection.ExecuteAsync(createRoleQuery, role, transaction);
        }

        public async Task Delete(string roleId, IDbTransaction transaction)
        {
            await _connection.ExecuteAsync(deleteRolQuery, new { Id = roleId }, transaction);
        }

        public async Task<Rol> GetRoleById(string roleId, IDbTransaction transaction)
        {
            return await _connection.QueryFirstOrDefaultAsync<Rol>(getRoleByIdQuery, new { Id = roleId });
        }

        public async Task Update(Rol role, IDbTransaction transaction)
        {
            await _connection.ExecuteAsync(updateRoleQuery, role, transaction);
        }

        private const string getRoleByRoleNameQuery = @"SELECT Id,Name,NormalizedName FROM Roles 
                                                    WHERE UPPER(NormalizedName) = UPPER(@NormalizedName)";
        private const string createRoleQuery = @"INSERT INTO ROLES (Id,Name,NormalizedName)
                                                VALUES(@Id,@Name,@NormalizedName)";
        private const string getAllQuery = @"SELECT * 
                                            FROM ROLES";
        private const string deleteRolQuery = @"DELETE FROM Roles
                                                WHERE Id=@Id";
        private const string getRoleByIdQuery = @"SELECT * FROM Roles
                                                WHERE Id=@Id";
        private const string updateRoleQuery = @"UPDATE ROLES SET Name=@Name,NormalizedName=@NormalizedName
                                                WHERE Id=@Id";
    }
}
