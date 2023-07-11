using MediatR;

namespace CineNet.Aplication.Commands
{
    public class UpdateCinemaCommand : IRequest<UpdateCinemaCommandResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string Status { get; set; }
    }
}
