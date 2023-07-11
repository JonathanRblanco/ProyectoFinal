using MediatR;

namespace CineNet.Aplication.Commands
{
    public class DeleteMovieCommand : IRequest<DeleteMovieCommandResponse>
    {
        public int id { get; set; }
    }
}
