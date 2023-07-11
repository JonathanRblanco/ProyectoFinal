using MediatR;

namespace CineNet.Aplication.Commands
{
    public class CreateGenderCommand : IRequest<CreateGenderCommandResponse>
    {
        public string Description { get; set; }
    }
}
