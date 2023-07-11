using MediatR;

namespace CineNet.Aplication.Commands
{
    public class DeleteShowCommand : IRequest<DeleteShowCommandResponse>
    {
        public int Id { get; set; }
    }
}
