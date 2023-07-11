using AutoMapper;
using CineNet.Aplication.Queries;
using CineNet.Domain.Contracts;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class GetRoomByIdQueryHandler : IRequestHandler<GetRoomByIdQuery, GetRoomByIdQueryResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetRoomByIdQueryHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<GetRoomByIdQueryResponse> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
        {
            var room = await unitOfWork.RoomsRepository.GetById(request.Id, unitOfWork.Transaction);
            return mapper.Map<GetRoomByIdQueryResponse>(room);
        }
    }
}
