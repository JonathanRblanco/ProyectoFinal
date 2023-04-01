using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProyectoFinal.Models;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.DTOs.Requests
{
    public class CreateUserRequest : IRequest<Result<User>>
    {
        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "El emai es requerido")]
        [EmailAddress(ErrorMessage = "Ingrese un email con un formato correcto!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "La contraseña es requerida")]
        public string Pass { get; set; }
    }
}
