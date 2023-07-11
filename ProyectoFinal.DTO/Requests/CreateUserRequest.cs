using Ardalis.Result;
using MediatR;
using ProyectoFinal.Data;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.DTO.Requests
{
    public class CreateUserRequest : IRequest<Result<User>>
    {
        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "El email es requerido")]
        [EmailAddress(ErrorMessage = "Ingrese un email con un formato correcto!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "La contraseña es requerida")]
        public string Pass { get; set; }
    }
}
