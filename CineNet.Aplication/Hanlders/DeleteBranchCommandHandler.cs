using AutoMapper;
using CineNet.Aplication.Commands;
using CineNet.Domain.Contracts;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class DeleteBranchCommandHandler : IRequestHandler<DeleteBranchCommand, DeleteBranchCommandResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DeleteBranchCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<DeleteBranchCommandResponse> Handle(DeleteBranchCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.BranchesRepository.Delete(request.Id, unitOfWork.Transaction);
            return new DeleteBranchCommandResponse();
        }
    }
}

