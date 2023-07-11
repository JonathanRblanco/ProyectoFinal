using AutoMapper;
using CineNet.Aplication.Queries;
using CineNet.Domain.Contracts;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class GetImageQueryHandler : IRequestHandler<GetImageQuery, GetImageQueryResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetImageQueryHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<GetImageQueryResponse> Handle(GetImageQuery request, CancellationToken cancellationToken)
        {
            var image = await unitOfWork.ImagesRepository.GetById(request.Id, unitOfWork.Transaction);
            return mapper.Map<GetImageQueryResponse>(image);
        }
    }
}
