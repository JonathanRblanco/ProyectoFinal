using Ardalis.Result;
using MediatR;

namespace ProyectoFinal.DTO.Requests
{
    public class DeleteRoomRequest : IRequest<Result>
    {
        public int Id { get; set; }
    }
}
