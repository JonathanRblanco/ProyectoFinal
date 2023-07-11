using AutoMapper;
using CineNet.Aplication.Queries;
using CineNet.Domain.Contracts;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class GetRoomsQueryHandler : IRequestHandler<GetRoomsQuery, IEnumerable<GetRoomsQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRoomsQueryHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetRoomsQueryResponse>> Handle(GetRoomsQuery request, CancellationToken cancellationToken)
        {
            var rooms = await _unitOfWork.RoomsRepository.GetAllAsync(_unitOfWork.Transaction);
            var result = _mapper.Map<IEnumerable<GetRoomsQueryResponse>>(rooms);
            return result;
        }
    }
}
