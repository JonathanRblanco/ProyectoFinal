using AutoMapper;
using CineNet.Aplication.Commands;
using CineNet.Domain.Contracts;
using CineNet.Domain.Entities;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class CreateClasificationCommandHandler : IRequestHandler<CreateClasificationCommand, CreateClasificationCommandResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CreateClasificationCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<CreateClasificationCommandResponse> Handle(CreateClasificationCommand request, CancellationToken cancellationToken)
        {
            var clasification = mapper.Map<Clasification>(request);
            clasification.Id = await unitOfWork.ClasificationsRepository.Create(clasification, unitOfWork.Transaction);
            return mapper.Map<CreateClasificationCommandResponse>(clasification);
        }
    }
}
