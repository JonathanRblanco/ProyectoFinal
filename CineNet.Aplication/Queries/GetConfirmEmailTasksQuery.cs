using MediatR;

namespace CineNet.Aplication.Queries
{
    public class GetConfirmEmailTasksQuery : IRequest<IEnumerable<GetConfirmEmailTasksQueryResponse>>
    {
    }
}
