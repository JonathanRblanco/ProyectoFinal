using CineNet.Aplication.Commands;
using CineNet.Domain.Contracts;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class DeleteCinemaCommandHandler : IRequestHandler<DeleteCinemaCommand, DeleteCinemaCommandResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        public DeleteCinemaCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<DeleteCinemaCommandResponse> Handle(DeleteCinemaCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.CinemasRepository.Delete(request.Id, unitOfWork.Transaction);
            return new DeleteCinemaCommandResponse();
        }
    }
}
