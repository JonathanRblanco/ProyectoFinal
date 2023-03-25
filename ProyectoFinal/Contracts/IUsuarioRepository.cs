using Microsoft.AspNetCore.Identity;
using ProyectoFinal.Models;
using System.Data;

namespace ProyectoFinal.Contracts
{
    public interface IUsuarioRepository
    {
        public Task<Usuario> GetUserByIdAsync(int Id);
        public Task<int> CreateUser(IdentityUser user, IDbTransaction transaction);
    }
}
