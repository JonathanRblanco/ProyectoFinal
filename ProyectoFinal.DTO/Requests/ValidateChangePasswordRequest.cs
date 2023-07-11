using Ardalis.Result;
using MediatR;

namespace ProyectoFinal.DTO.Requests
{
    public class ValidateChangePasswordRequest : IRequest<Result>
    {
        public string UserEmail { get; set; }
    }
}
