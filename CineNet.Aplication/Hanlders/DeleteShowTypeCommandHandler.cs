using CineNet.Aplication.Commands;
using CineNet.Domain.Contracts;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class DeleteShowTypeCommandHandler : IRequestHandler<DeleteShowTypeCommand, DeleteShowTypeCommandResponse>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteShowTypeCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<DeleteShowTypeCommandResponse> Handle(DeleteShowTypeCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.ShowsTypeRepository.Delete(request.Id, unitOfWork.Transaction);
            return new DeleteShowTypeCommandResponse();
        }
    }
}
