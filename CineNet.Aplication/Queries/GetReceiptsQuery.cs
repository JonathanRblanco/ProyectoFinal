using MediatR;

namespace CineNet.Aplication.Queries
{
    public class GetReceiptsQuery : IRequest<IEnumerable<GetReceiptsQueryResponse>>
    {
    }
}
