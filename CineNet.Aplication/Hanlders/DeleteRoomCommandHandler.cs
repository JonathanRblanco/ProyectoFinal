using CineNet.Aplication.Commands;
using CineNet.Domain.Contracts;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand, DeleteRoomCommandResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        public DeleteRoomCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<DeleteRoomCommandResponse> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.RoomsRepository.Delete(request.Id, unitOfWork.Transaction);
            return new DeleteRoomCommandResponse();
        }
    }
}
