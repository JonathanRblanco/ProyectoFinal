using MediatR;

namespace CineNet.Aplication.Queries
{
    public class GetMoviesQuery : IRequest<IEnumerable<GetMoviesQueryResponse>>
    {
    }
}
