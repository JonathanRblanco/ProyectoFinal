using MediatR;

namespace CineNet.Aplication.Commands
{
    public class DeleteRoomCommand : IRequest<DeleteRoomCommandResponse>
    {
        public int Id { get; set; }
    }
}
