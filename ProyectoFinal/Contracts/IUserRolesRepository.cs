using System.Data;

namespace ProyectoFinal.Contracts
{
    public interface IUserRolesRepository
    {
        public Task InsertUsuRol(string roleId, string usuId, IDbTransaction transaction);
    }
}
