using MediatR;

namespace CineNet.Aplication.Commands
{
    public class CreateEmailTaskCommand : IRequest<CreateEmailTaskCommandResponse>
    {
        public string Data { get; set; }
        public string Type { get; set; }
    }
}
