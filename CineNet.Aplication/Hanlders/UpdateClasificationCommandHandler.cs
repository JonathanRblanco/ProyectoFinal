using AutoMapper;
using CineNet.Aplication.Commands;
using CineNet.Domain.Contracts;
using CineNet.Domain.Entities;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class UpdateClasificationCommandHandler : IRequestHandler<UpdateClasificationCommand, UpdateClasificationCommandResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UpdateClasificationCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<UpdateClasificationCommandResponse> Handle(UpdateClasificationCommand request, CancellationToken cancellationToken)
        {
            var clasification = mapper.Map<Clasification>(request);
            await unitOfWork.ClasificationsRepository.Update(clasification, unitOfWork.Transaction);
            return mapper.Map<UpdateClasificationCommandResponse>(clasification);
        }
    }
}
