using AutoMapper;
using CineNet.Aplication.Queries;
using CineNet.Domain.Contracts;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class GetChangePasswordTasksQueryHandler : IRequestHandler<GetChangePasswordTasksQuery, IEnumerable<GetChangePasswordTasksQueryResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetChangePasswordTasksQueryHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<GetChangePasswordTasksQueryResponse>> Handle(GetChangePasswordTasksQuery request, CancellationToken cancellationToken)
        {
            var tasks = await unitOfWork.EmailTasksRepository.Get(unitOfWork.Transaction);
            return mapper.Map<IEnumerable<GetChangePasswordTasksQueryResponse>>(tasks.Where(t => t.Type == "ChangePassword"));
        }
    }
}
