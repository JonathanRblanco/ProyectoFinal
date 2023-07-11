using AutoMapper;
using CineNet.Aplication.Queries;
using CineNet.Domain.Contracts;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    internal class GetBranchesQueryHandler : IRequestHandler<GetBranchesQuery, IEnumerable<GetBranchesQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBranchesQueryHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetBranchesQueryResponse>> Handle(GetBranchesQuery request, CancellationToken cancellationToken)
        {
            var cinemasBranches = await _unitOfWork.BranchesRepository.GetAllBranches(_unitOfWork.Transaction);
            var response = _mapper.Map<IEnumerable<GetBranchesQueryResponse>>(cinemasBranches);
            return response;
        }
    }
}
