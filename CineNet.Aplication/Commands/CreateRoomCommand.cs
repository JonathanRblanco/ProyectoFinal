using MediatR;

namespace CineNet.Aplication.Commands
{
    public class CreateRoomCommand : IRequest<CreateRoomCommandResponse>
    {
        public int Number { get; set; }
        public int Capacity { get; set; }
        public int CinemaBranchId { get; set; }
    }
}
