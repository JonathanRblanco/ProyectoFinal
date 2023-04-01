using Ardalis.Result;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.DTOs.Requests
{
    public class SignInUserRequest : IRequest<Result>
    {
        [Required(ErrorMessage = "El campo nombre es requerido")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "La contraseña es requerida")]
        public string Password { get; set; }
    }
}
