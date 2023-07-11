using AutoMapper;
using CineNet.Aplication.Queries;
using CineNet.Domain.Contracts;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class GetClasificationsQueryHandler : IRequestHandler<GetClasificationsQuery, IEnumerable<GetClasificationsQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetClasificationsQueryHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetClasificationsQueryResponse>> Handle(GetClasificationsQuery request, CancellationToken cancellationToken)
        {
            var clasifications = await _unitOfWork.ClasificationsRepository.GetAllAsync(_unitOfWork.Transaction);
            return _mapper.Map<IEnumerable<GetClasificationsQueryResponse>>(clasifications);
        }
    }
}
