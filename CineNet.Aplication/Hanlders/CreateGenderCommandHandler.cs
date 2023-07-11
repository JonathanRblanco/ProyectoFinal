using AutoMapper;
using CineNet.Aplication.Commands;
using CineNet.Domain.Contracts;
using CineNet.Domain.Entities;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class CreateGenderCommandHandler : IRequestHandler<CreateGenderCommand, CreateGenderCommandResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CreateGenderCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<CreateGenderCommandResponse> Handle(CreateGenderCommand request, CancellationToken cancellationToken)
        {
            var gender = mapper.Map<Gender>(request);
            gender.Id = await unitOfWork.GendersRepository.Create(gender, unitOfWork.Transaction);
            return mapper.Map<CreateGenderCommandResponse>(gender);
        }
    }
}
