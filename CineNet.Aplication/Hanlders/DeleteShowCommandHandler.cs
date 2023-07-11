using CineNet.Aplication.Commands;
using CineNet.Domain.Contracts;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class DeleteShowCommandHandler : IRequestHandler<DeleteShowCommand, DeleteShowCommandResponse>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteShowCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<DeleteShowCommandResponse> Handle(DeleteShowCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.ShowsRepository.Delete(request.Id, unitOfWork.Transaction);
            return new DeleteShowCommandResponse();
        }
    }
}
