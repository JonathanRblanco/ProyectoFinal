using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.DTOs.Commands.Requests
{
    public class SignInUserCommandRequest : IRequest<bool>
    {
        [Required(ErrorMessage = "El campo nombre es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "La contraseña es requerida")]
        public string Password { get; set; }
    }
}
