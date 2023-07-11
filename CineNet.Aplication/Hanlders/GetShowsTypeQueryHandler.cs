using AutoMapper;
using CineNet.Aplication.Queries;
using CineNet.Domain.Contracts;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class GetShowsTypeQueryHandler : IRequestHandler<GetShowsTypeQuery, IEnumerable<GetShowsTypeQueryResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetShowsTypeQueryHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<GetShowsTypeQueryResponse>> Handle(GetShowsTypeQuery request, CancellationToken cancellationToken)
        {
            var showsType = await unitOfWork.ShowsTypeRepository.GetAllAsync(unitOfWork.Transaction);
            return mapper.Map<IEnumerable<GetShowsTypeQueryResponse>>(showsType);
        }
    }
}
