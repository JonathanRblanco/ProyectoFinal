using CineNet.Aplication.Commands;
using CineNet.Domain.Contracts;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, DeleteTaskCommandResponse>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteTaskCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<DeleteTaskCommandResponse> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.EmailTasksRepository.Delete(request.Id, unitOfWork.Transaction);
            return new DeleteTaskCommandResponse();
        }
    }
}
