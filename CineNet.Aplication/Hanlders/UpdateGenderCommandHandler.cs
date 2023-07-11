using AutoMapper;
using CineNet.Aplication.Commands;
using CineNet.Domain.Contracts;
using CineNet.Domain.Entities;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class UpdateGenderCommandHandler : IRequestHandler<UpdateGenderCommand, UpdateGenderCommandResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UpdateGenderCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<UpdateGenderCommandResponse> Handle(UpdateGenderCommand request, CancellationToken cancellationToken)
        {
            var gender = mapper.Map<Gender>(request);
            await unitOfWork.GendersRepository.Update(gender, unitOfWork.Transaction);
            return mapper.Map<UpdateGenderCommandResponse>(gender);
        }
    }
}
