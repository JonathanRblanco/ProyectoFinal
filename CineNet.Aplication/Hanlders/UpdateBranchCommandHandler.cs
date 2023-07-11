using AutoMapper;
using CineNet.Aplication.Commands;
using CineNet.Domain.Contracts;
using CineNet.Domain.Entities;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class UpdateBranchCommandHandler : IRequestHandler<UpdateBranchCommand, UpdateBranchCommandResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UpdateBranchCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<UpdateBranchCommandResponse> Handle(UpdateBranchCommand request, CancellationToken cancellationToken)
        {
            var branch = mapper.Map<Branch>(request);
            await unitOfWork.BranchesRepository.Update(branch, unitOfWork.Transaction);
            return mapper.Map<UpdateBranchCommandResponse>(branch);
        }
    }
}
