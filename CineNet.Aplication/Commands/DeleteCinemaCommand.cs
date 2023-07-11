using MediatR;

namespace CineNet.Aplication.Commands
{
    public class DeleteCinemaCommand : IRequest<DeleteCinemaCommandResponse>
    {
        public int Id { get; set; }
    }
}
