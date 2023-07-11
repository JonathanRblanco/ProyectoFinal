using MediatR;

namespace CineNet.Aplication.Commands
{
    public class DeleteBranchCommand : IRequest<DeleteBranchCommandResponse>
    {
        public int Id { get; set; }
    }
}
