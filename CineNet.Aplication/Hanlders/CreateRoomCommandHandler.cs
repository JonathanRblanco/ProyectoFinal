using AutoMapper;
using CineNet.Aplication.Commands;
using CineNet.Domain.Contracts;
using CineNet.Domain.Entities;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, CreateRoomCommandResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CreateRoomCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<CreateRoomCommandResponse> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            var room = mapper.Map<Room>(request);
            room.Id = await unitOfWork.RoomsRepository.Create(room, unitOfWork.Transaction);
            return mapper.Map<CreateRoomCommandResponse>(room);
        }
    }
}
