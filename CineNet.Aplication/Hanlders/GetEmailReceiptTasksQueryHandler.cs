using AutoMapper;
using CineNet.Aplication.Queries;
using CineNet.Domain.Contracts;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class GetEmailReceiptTasksQueryHandler : IRequestHandler<GetEmailReceiptTasksQuery, IEnumerable<GetEmailReceiptTasksQueryResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetEmailReceiptTasksQueryHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<GetEmailReceiptTasksQueryResponse>> Handle(GetEmailReceiptTasksQuery request, CancellationToken cancellationToken)
        {
            var tasks = await unitOfWork.EmailTasksRepository.Get(unitOfWork.Transaction);
            return mapper.Map<IEnumerable<GetEmailReceiptTasksQueryResponse>>(tasks.Where(t => t.Type == "SendReceipt"));
        }
    }
}
