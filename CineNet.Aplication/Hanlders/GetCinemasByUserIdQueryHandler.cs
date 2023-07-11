using AutoMapper;
using CineNet.Aplication.Queries;
using CineNet.Domain.Contracts;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class GetCinemasByUserIdQueryHandler : IRequestHandler<GetCinemasByUserIdQuery, IEnumerable<GetCinemasByUserIdQueryResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetCinemasByUserIdQueryHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<GetCinemasByUserIdQueryResponse>> Handle(GetCinemasByUserIdQuery request, CancellationToken cancellationToken)
        {
            var cinemas = await unitOfWork.CinemasRepository.GetCinemasByUserId(request.UserId, unitOfWork.Transaction);
            var response = mapper.Map<IEnumerable<GetCinemasByUserIdQueryResponse>>(cinemas);
            return response;
        }
    }
}
