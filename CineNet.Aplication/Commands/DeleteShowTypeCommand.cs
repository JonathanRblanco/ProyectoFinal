using MediatR;

namespace CineNet.Aplication.Commands
{
    public class DeleteShowTypeCommand : IRequest<DeleteShowTypeCommandResponse>
    {
        public int Id { get; set; }
    }
}
