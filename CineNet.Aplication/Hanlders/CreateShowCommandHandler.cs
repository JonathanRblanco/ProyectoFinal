using AutoMapper;
using CineNet.Aplication.Commands;
using CineNet.Domain.Contracts;
using CineNet.Domain.Entities;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class CreateShowCommandHandler : IRequestHandler<CreateShowCommand, CreateShowCommandResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CreateShowCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<CreateShowCommandResponse> Handle(CreateShowCommand request, CancellationToken cancellationToken)
        {
            var show = mapper.Map<Show>(request);
            show.Id = await unitOfWork.ShowsRepository.Create(show, unitOfWork.Transaction);
            return mapper.Map<CreateShowCommandResponse>(show);
        }
    }
}
