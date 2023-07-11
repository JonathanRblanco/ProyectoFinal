using MediatR;

namespace CineNet.Aplication.Commands
{
    public class UpdateRoomCommand : IRequest<UpdateRoomCommandResponse>
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int Capacity { get; set; }
        public int CinemaBranchId { get; set; }
    }
}
