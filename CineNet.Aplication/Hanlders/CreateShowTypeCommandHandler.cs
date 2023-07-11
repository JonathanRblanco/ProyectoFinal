using AutoMapper;
using CineNet.Aplication.Commands;
using CineNet.Domain.Contracts;
using CineNet.Domain.Entities;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class CreateShowTypeCommandHandler : IRequestHandler<CreateShowTypeCommand, CreateShowTypeCommandResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CreateShowTypeCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<CreateShowTypeCommandResponse> Handle(CreateShowTypeCommand request, CancellationToken cancellationToken)
        {
            var showType = mapper.Map<ShowType>(request);
            showType.Id = await unitOfWork.ShowsTypeRepository.Create(showType, unitOfWork.Transaction);
            return mapper.Map<CreateShowTypeCommandResponse>(showType);
        }
    }
}
