using AutoMapper;
using CineNet.Aplication.Queries;
using CineNet.Domain.Contracts;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class GetGenderByIdHandler : IRequestHandler<GetGenderByIdQuery, GetGenderByIdQueryResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetGenderByIdHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<GetGenderByIdQueryResponse> Handle(GetGenderByIdQuery request, CancellationToken cancellationToken)
        {
            var gender = await unitOfWork.GendersRepository.GetById(request.Id, unitOfWork.Transaction);
            return mapper.Map<GetGenderByIdQueryResponse>(gender);
        }
    }
}
