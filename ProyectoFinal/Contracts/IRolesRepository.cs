using System.Data;

namespace ProyectoFinal.Contracts
{
    public interface IRolesRepository
    {
        public Task<string> GetRoleByRoleName(string roleName, IDbTransaction transaction);
    }
}
