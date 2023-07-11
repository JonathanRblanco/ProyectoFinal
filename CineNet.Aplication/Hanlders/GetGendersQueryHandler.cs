using AutoMapper;
using CineNet.Aplication.Queries;
using CineNet.Domain.Contracts;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class GetGendersQueryHandler : IRequestHandler<GetGendersQuery, IEnumerable<GetGendersQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetGendersQueryHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetGendersQueryResponse>> Handle(GetGendersQuery request, CancellationToken cancellationToken)
        {
            var genders = await _unitOfWork.GendersRepository.GetAllAsync(_unitOfWork.Transaction);
            return _mapper.Map<IEnumerable<GetGendersQueryResponse>>(genders);
        }
    }
}
