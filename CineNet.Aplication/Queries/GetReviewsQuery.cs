using MediatR;

namespace CineNet.Aplication.Queries
{
    public class GetReviewsQuery : IRequest<IEnumerable<GetReviewsQueryResponse>>
    {
    }
}
