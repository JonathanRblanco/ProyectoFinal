using AutoMapper;
using CineNet.Aplication.Commands;
using CineNet.Domain.Contracts;
using CineNet.Domain.Entities;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class UpdateShowCommandHandler : IRequestHandler<UpdateShowCommand, UpdateShowCommandResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UpdateShowCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<UpdateShowCommandResponse> Handle(UpdateShowCommand request, CancellationToken cancellationToken)
        {
            var show = mapper.Map<Show>(request);
            await unitOfWork.ShowsRepository.Update(show, unitOfWork.Transaction);
            return mapper.Map<UpdateShowCommandResponse>(show);
        }
    }
}
