using MediatR;

namespace CineNet.Aplication.Commands
{
    public class UpdateShowTypeCommand : IRequest<UpdateShowTypeCommandResponse>
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
