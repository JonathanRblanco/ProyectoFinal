using MediatR;

namespace CineNet.Aplication.Commands
{
    public class AssignCinemaByUserIdCommand : IRequest<AssignCinemaByUserIdCommandResponse>
    {
        public int[] Ids { get; set; }
        public string UserId { get; set; }
    }
}
