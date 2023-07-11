using AutoMapper;
using CineNet.Aplication.Commands;
using CineNet.Domain.Contracts;
using CineNet.Domain.Entities;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class CreateCinemaCommandHandler : IRequestHandler<CreateCinemaCommand, CreateCinemaCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateCinemaCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CreateCinemaCommandResponse> Handle(CreateCinemaCommand request, CancellationToken cancellationToken)
        {
            var cinema = _mapper.Map<Cinema>(request);
            cinema.Id = await _unitOfWork.CinemasRepository.CreateCine(cinema, _unitOfWork.Transaction);
            return _mapper.Map<CreateCinemaCommandResponse>(cinema);
        }
    }
}
