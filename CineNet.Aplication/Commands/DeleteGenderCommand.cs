using MediatR;

namespace CineNet.Aplication.Commands
{
    public class DeleteGenderCommand : IRequest<DeleteGenderCommandResponse>
    {
        public int Id { get; set; }
    }
}
