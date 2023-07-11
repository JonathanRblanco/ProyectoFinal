using ProyectoFinal.Data;

namespace ProyectoFinal.DTO.Responses
{
    public class EditUserResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool LockoutEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public IEnumerable<Rol> AllRoles { get; set; }
        public IEnumerable<string> UsuRoles { get; set; }
    }
}
