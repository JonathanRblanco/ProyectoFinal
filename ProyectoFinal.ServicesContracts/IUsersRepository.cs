using ProyectoFinal.Data;
using System.Data;

namespace ProyectoFinal.ServicesContracts
{
    public interface IUsersRepository
    {
        public Task<User> GetUserByIdAsync(Guid Id, IDbTransaction transaction);
        public Task<int> CreateUser(User user, IDbTransaction transaction);
        public Task<IEnumerable<User>> GetAllUsersAsync(IDbTransaction transaction);
        public Task Delete(string id, IDbTransaction transaction);
        public Task ChangeImage(int imageId, string userId, IDbTransaction transaction);
        public Task<User> FindByEmail(string normalizedEmail, IDbTransaction transaction);
        public Task<User> GetUserByLogin(string loginProvider, string providerKey, IDbTransaction transaction);
    }
}