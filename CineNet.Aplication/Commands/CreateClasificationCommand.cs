using MediatR;

namespace CineNet.Aplication.Commands
{
    public class CreateClasificationCommand : IRequest<CreateClasificationCommandResponse>
    {
        public string Description { get; set; }
    }
}
