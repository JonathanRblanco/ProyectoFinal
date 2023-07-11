using MediatR;

namespace CineNet.Aplication.Commands
{
    public class UpdateGenderCommand : IRequest<UpdateGenderCommandResponse>
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
