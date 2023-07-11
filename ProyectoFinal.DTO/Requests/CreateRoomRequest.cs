using Ardalis.Result;
using MediatR;

namespace ProyectoFinal.DTO.Requests
{
    public class CreateRoomRequest : IRequest<Result>
    {
        public int Number { get; set; }
        public int Capacity { get; set; }
        public int CinemaBranchId { get; set; }
    }
}
