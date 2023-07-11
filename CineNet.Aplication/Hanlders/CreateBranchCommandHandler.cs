using AutoMapper;
using CineNet.Aplication.Commands;
using CineNet.Domain.Contracts;
using CineNet.Domain.Entities;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class CreateBranchCommandHandler : IRequestHandler<CreateBranchCommand, CreateBranchCommandResponse>
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;

        public CreateBranchCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreateBranchCommandResponse> Handle(CreateBranchCommand request, CancellationToken cancellationToken)
        {
            var cinemaBranch = _mapper.Map<Branch>(request);
            cinemaBranch.Id = await _unitOfWork.BranchesRepository.CreateBranch(cinemaBranch, _unitOfWork.Transaction);
            return _mapper.Map<CreateBranchCommandResponse>(cinemaBranch);
        }
    }
}
