using AutoMapper;
using CineNet.Aplication.Queries;
using CineNet.Domain.Contracts;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class GetBranchByIdHandler : IRequestHandler<GetBranchByIdQuery, GetBranchByIdQueryResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetBranchByIdHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<GetBranchByIdQueryResponse> Handle(GetBranchByIdQuery request, CancellationToken cancellationToken)
        {
            var cinemaBranch = await unitOfWork.BranchesRepository.GetById(request.Id, unitOfWork.Transaction);
            return mapper.Map<GetBranchByIdQueryResponse>(cinemaBranch);
        }
    }
}
