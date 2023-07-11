using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Responses;

namespace ProyectoFinal.DTO.Requests
{
    public class EditUserRequest : IRequest<Result<EditUserResponse>>
    {
        public string UserId { get; set; }
    }
}
