using MediatR;

namespace CineNet.Aplication.Commands
{
    public class UpdateClasificationCommand : IRequest<UpdateClasificationCommandResponse>
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
