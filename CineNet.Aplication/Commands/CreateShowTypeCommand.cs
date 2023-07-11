using MediatR;

namespace CineNet.Aplication.Commands
{
    public class CreateShowTypeCommand : IRequest<CreateShowTypeCommandResponse>
    {
        public string Description { get; set; }
    }
}
