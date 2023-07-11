using AutoMapper;
using CineNet.Aplication.Queries;
using CineNet.Domain.Contracts;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class GetConfirmEmailTasksQueryHandler : IRequestHandler<GetConfirmEmailTasksQuery, IEnumerable<GetConfirmEmailTasksQueryResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetConfirmEmailTasksQueryHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<GetConfirmEmailTasksQueryResponse>> Handle(GetConfirmEmailTasksQuery request, CancellationToken cancellationToken)
        {
            var tasks = await unitOfWork.EmailTasksRepository.Get(unitOfWork.Transaction);
            return mapper.Map<IEnumerable<GetConfirmEmailTasksQueryResponse>>(tasks.Where(t => t.Type == "ConfirmEmail"));
        }
    }
}
