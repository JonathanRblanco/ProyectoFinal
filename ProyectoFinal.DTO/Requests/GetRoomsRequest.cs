using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Responses;

namespace ProyectoFinal.DTO.Requests
{
    public class GetRoomsRequest : IRequest<Result<IEnumerable<GetRoomsResponse>>>
    {
        public int? BranchId { get; set; }
        public int? CinemaId { get; set; }
    }
}
