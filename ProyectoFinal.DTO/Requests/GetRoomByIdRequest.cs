using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Responses;

namespace ProyectoFinal.DTO.Requests
{
    public class GetRoomByIdRequest : IRequest<Result<GetRoomByIdResponse>>
    {
        public int Id { get; set; }
    }
}
