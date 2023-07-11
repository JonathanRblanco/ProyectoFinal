using MediatR;

namespace CineNet.Aplication.Queries
{
    public class GetGendersQuery : IRequest<IEnumerable<GetGendersQueryResponse>>
    {
    }
}
