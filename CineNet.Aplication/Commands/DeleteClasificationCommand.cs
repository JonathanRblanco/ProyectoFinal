using MediatR;

namespace CineNet.Aplication.Commands
{
    public class DeleteClasificationCommand : IRequest<DeleteClasificationCommandResponse>
    {
        public int Id { get; set; }
    }
}
