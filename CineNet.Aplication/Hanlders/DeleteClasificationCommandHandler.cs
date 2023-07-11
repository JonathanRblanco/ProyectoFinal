using CineNet.Aplication.Commands;
using CineNet.Domain.Contracts;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class DeleteClasificationCommandHandler : IRequestHandler<DeleteClasificationCommand, DeleteClasificationCommandResponse>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteClasificationCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<DeleteClasificationCommandResponse> Handle(DeleteClasificationCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.ClasificationsRepository.Delete(request.Id, unitOfWork.Transaction);
            return new DeleteClasificationCommandResponse();
        }
    }
}
