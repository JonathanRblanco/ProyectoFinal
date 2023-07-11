using AutoMapper;
using CineNet.Aplication.Queries;
using CineNet.Domain.Contracts;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class GetStatisticsQueryHandler : IRequestHandler<GetStatisticsQuery, IEnumerable<GetStatisticsQueryResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetStatisticsQueryHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<GetStatisticsQueryResponse>> Handle(GetStatisticsQuery request, CancellationToken cancellationToken)
        {
            var statistics = await unitOfWork.CinemasRepository.GetStatistics(request.CinemaId, request.Year, request.Branch, unitOfWork.Transaction);
            return mapper.Map<IEnumerable<GetStatisticsQueryResponse>>(statistics);
        }
    }
}
