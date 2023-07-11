using AutoMapper;
using CineNet.Aplication.Commands;
using CineNet.Domain.Contracts;
using CineNet.Domain.Entities;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand, UpdateRoomCommandResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UpdateRoomCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<UpdateRoomCommandResponse> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            var room = mapper.Map<Room>(request);
            await unitOfWork.RoomsRepository.Update(room, unitOfWork.Transaction);
            return mapper.Map<UpdateRoomCommandResponse>(room);
        }
    }
}
