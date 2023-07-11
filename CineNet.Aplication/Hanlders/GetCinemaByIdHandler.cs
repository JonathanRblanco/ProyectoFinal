using AutoMapper;
using CineNet.Aplication.Queries;
using CineNet.Domain.Contracts;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class GetCinemaByIdHandler : IRequestHandler<GetCinemaByIdQuery, GetCinemaByIdQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCinemaByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GetCinemaByIdQueryResponse> Handle(GetCinemaByIdQuery request, CancellationToken cancellationToken)
        {
            var cinema = await _unitOfWork.CinemasRepository.GetById(request.Id, _unitOfWork.Transaction);
            return _mapper.Map<GetCinemaByIdQueryResponse>(cinema);
        }
    }
}
