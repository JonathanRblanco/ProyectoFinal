using MediatR;

namespace CineNet.Aplication.Commands
{
    public class DeleteTaskCommand : IRequest<DeleteTaskCommandResponse>
    {
        public int Id { get; set; }
    }
}
