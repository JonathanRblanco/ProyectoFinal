using AutoMapper;
using CineNet.Domain.Contracts;
using CineNet.Domain.Entities;
using MediatR;

namespace CineNet.Aplication.Commands
{
    public class UpdateShowTypeCommandHandler : IRequestHandler<UpdateShowTypeCommand, UpdateShowTypeCommandResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UpdateShowTypeCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<UpdateShowTypeCommandResponse> Handle(UpdateShowTypeCommand request, CancellationToken cancellationToken)
        {
            var showType = mapper.Map<ShowType>(request);
            await unitOfWork.ShowsTypeRepository.Update(showType, unitOfWork.Transaction);
            return mapper.Map<UpdateShowTypeCommandResponse>(showType);
        }
    }
}
