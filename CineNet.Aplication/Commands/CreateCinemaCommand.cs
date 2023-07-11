using MediatR;

namespace CineNet.Aplication.Commands
{
    public class CreateCinemaCommand : IRequest<CreateCinemaCommandResponse>
    {
        public string Name { get; set; }
        public string URL { get; set; }
    }
}
