using MediatR;

namespace CineNet.Aplication.Queries
{
    public class GetShowsQuery : IRequest<IEnumerable<GetShowsQueryResponse>>
    {
    }
}
