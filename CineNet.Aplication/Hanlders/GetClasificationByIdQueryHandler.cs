using AutoMapper;
using CineNet.Aplication.Queries;
using CineNet.Domain.Contracts;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class GetClasificationByIdQueryHandler : IRequestHandler<GetClasificationByIdQuery, GetClasificationByIdQueryResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetClasificationByIdQueryHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<GetClasificationByIdQueryResponse> Handle(GetClasificationByIdQuery request, CancellationToken cancellationToken)
        {
            var clasification = await unitOfWork.ClasificationsRepository.GetById(request.Id, unitOfWork.Transaction);
            return mapper.Map<GetClasificationByIdQueryResponse>(clasification);
        }
    }
}
