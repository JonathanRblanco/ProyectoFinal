using MediatR;

namespace CineNet.Aplication.Queries
{
    public class GetRoomsQuery : IRequest<IEnumerable<GetRoomsQueryResponse>>
    {

    }
}
