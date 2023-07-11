using CineNet.Aplication.Commands;
using CineNet.Domain.Contracts;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class DeleteGenderCommandHandler : IRequestHandler<DeleteGenderCommand, DeleteGenderCommandResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        public DeleteGenderCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<DeleteGenderCommandResponse> Handle(DeleteGenderCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.GendersRepository.Delete(request.Id, unitOfWork.Transaction);
            return new DeleteGenderCommandResponse();
        }
    }
}
