using Microsoft.AspNetCore.Identity;

namespace ProyectoFinal.Data
{
    public class UserLogin : UserLoginInfo
    {
        public UserLogin(string loginProvider, string providerKey, string? displayName, string userId) : base(loginProvider, providerKey, displayName)
        {
            UserId = userId;
        }
        public string UserId { get; set; }
    }
}
