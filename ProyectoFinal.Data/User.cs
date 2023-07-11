using Microsoft.AspNetCore.Identity;

namespace ProyectoFinal.Data
{
    public class User : IdentityUser
    {
        public int ImageId { get; set; }
    }
}
