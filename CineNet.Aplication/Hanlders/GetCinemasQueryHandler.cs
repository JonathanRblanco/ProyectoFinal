using AutoMapper;
using CineNet.Aplication.Queries;
using CineNet.Domain.Contracts;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class GetCinemasQueryHandler : IRequestHandler<GetCinemasQuery, IEnumerable<GetCinemasQueryResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetCinemasQueryHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<GetCinemasQueryResponse>> Handle(GetCinemasQuery request, CancellationToken cancellationToken)
        {
            var cinemas = await unitOfWork.CinemasRepository.GetAllCines(unitOfWork.Transaction);
            var response = mapper.Map<IEnumerable<GetCinemasQueryResponse>>(cinemas);
            return response;
        }
    }
}
