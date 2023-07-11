using Ardalis.Result;
using MediatR;

namespace ProyectoFinal.DTO.Requests
{
    public class ConfirmEmailRequest : IRequest<Result>
    {
        public string UserId { get; set; }
        public string Token { get; set; }
    }
}
