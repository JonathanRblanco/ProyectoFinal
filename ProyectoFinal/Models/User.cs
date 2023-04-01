using Microsoft.AspNetCore.Identity;

namespace ProyectoFinal.Models
{
    public class User:IdentityUser
    {
        public Byte[] ProfileImage { get; set; }
    }
}
