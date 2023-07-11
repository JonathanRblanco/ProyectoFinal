using AutoMapper;
using CineNet.Aplication.Queries;
using CineNet.Domain.Contracts;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class GetReviewsQueryHandler : IRequestHandler<GetReviewsQuery, IEnumerable<GetReviewsQueryResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetReviewsQueryHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<GetReviewsQueryResponse>> Handle(GetReviewsQuery request, CancellationToken cancellationToken)
        {
            var reviews = await unitOfWork.ReviewsRepository.GetAllAsync(unitOfWork.Transaction);
            var response = mapper.Map<IEnumerable<GetReviewsQueryResponse>>(reviews);
            return response;
        }
    }
}
