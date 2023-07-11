using MediatR;

namespace CineNet.Aplication.Queries
{
    public class GetEmailReceiptTasksQuery : IRequest<IEnumerable<GetEmailReceiptTasksQueryResponse>>
    {
    }
}
