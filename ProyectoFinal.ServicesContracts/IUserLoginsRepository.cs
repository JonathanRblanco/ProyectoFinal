using ProyectoFinal.Data;
using System.Data;

namespace ProyectoFinal.ServicesContracts
{
    public interface IUserLoginsRepository
    {
        public Task<IEnumerable<UserLogin>> GetLoginsByUserId(string userId, IDbTransaction transaction);
        public Task AddLogin(string userId, string loginProvider, string loginKey, string providerName, IDbTransaction transaction);
        public Task RemoveLogin(string userId, string loginProvider, IDbTransaction transaction);
    }
}
