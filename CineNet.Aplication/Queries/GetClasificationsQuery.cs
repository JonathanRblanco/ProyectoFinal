using MediatR;

namespace CineNet.Aplication.Queries
{
    public class GetClasificationsQuery : IRequest<IEnumerable<GetClasificationsQueryResponse>>
    {
    }
}
