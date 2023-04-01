using ProyectoFinal.Models;
using System.Data;

namespace ProyectoFinal.Contracts
{
    public interface IUsuarioRepository
    {
        public Task<User> GetUserByIdAsync(int Id);
        public Task<int> CreateUser(User user, IDbTransaction transaction);
    }
}
