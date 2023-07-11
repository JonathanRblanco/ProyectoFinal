using AutoMapper;
using CineNet.Aplication.Queries;
using CineNet.Domain.Contracts;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class GetShowTypeByIdQueryHandler : IRequestHandler<GetShowTypeByIdQuery, GetShowTypeByIdQueryResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetShowTypeByIdQueryHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<GetShowTypeByIdQueryResponse> Handle(GetShowTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var showType = await unitOfWork.ShowsTypeRepository.GetById(request.Id, unitOfWork.Transaction);
            return mapper.Map<GetShowTypeByIdQueryResponse>(showType);
        }
    }
}
