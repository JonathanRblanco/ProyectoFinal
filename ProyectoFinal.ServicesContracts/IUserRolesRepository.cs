using ProyectoFinal.Data;
using System.Data;

namespace ProyectoFinal.ServicesContracts
{
    public interface IUserRolesRepository
    {
        public Task InsertUsuRol(UserRoles userRoles, IDbTransaction transaction);
    }
}
