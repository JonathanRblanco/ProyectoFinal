using AutoMapper;
using CineNet.Aplication.Commands;
using CineNet.Domain.Contracts;
using CineNet.Domain.Entities;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class UpdateCinemaCommandHandler : IRequestHandler<UpdateCinemaCommand, UpdateCinemaCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateCinemaCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<UpdateCinemaCommandResponse> Handle(UpdateCinemaCommand request, CancellationToken cancellationToken)
        {
            var cinema = _mapper.Map<Cinema>(request);
            await _unitOfWork.CinemasRepository.Update(cinema, _unitOfWork.Transaction);
            return _mapper.Map<UpdateCinemaCommandResponse>(cinema);
        }
    }
}
