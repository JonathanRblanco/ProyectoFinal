using Ardalis.Result;
using MediatR;

namespace ProyectoFinal.DTO.Requests
{
    public class DeleteUserRequest : IRequest<Result>
    {
        public string UserId { get; set; }
    }
}
