using MediatR;

namespace CineNet.Aplication.Queries
{
    public class GetChangePasswordTasksQuery : IRequest<IEnumerable<GetChangePasswordTasksQueryResponse>>
    {
    }
}
