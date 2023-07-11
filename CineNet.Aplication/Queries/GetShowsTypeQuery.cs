using MediatR;

namespace CineNet.Aplication.Queries
{
    public class GetShowsTypeQuery : IRequest<IEnumerable<GetShowsTypeQueryResponse>>
    {
    }
}
