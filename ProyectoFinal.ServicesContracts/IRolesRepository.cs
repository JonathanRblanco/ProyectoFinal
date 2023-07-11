using ProyectoFinal.Data;
using System.Data;

namespace ProyectoFinal.ServicesContracts
{
    public interface IRolesRepository
    {
        public IQueryable<Rol> GetAll(IDbTransaction transaction);
        public Task Create(Rol role, IDbTransaction transaction);
        public Task Delete(string roleId, IDbTransaction transaction);
        public Task<Rol> GetRoleById(string roleId, IDbTransaction transaction);
        public Task<Rol> GetRoleByName(string roleName, IDbTransaction transaction);
        public Task Update(Rol role, IDbTransaction transaction);
    }
}
