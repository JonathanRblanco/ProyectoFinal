using MediatR;

namespace CineNet.Aplication.Queries
{
    public class GetBranchesQuery : IRequest<IEnumerable<GetBranchesQueryResponse>>
    {
    }
}
