using MediatR;

namespace CineNet.Aplication.Queries
{
    public class GetCinemasQuery : IRequest<IEnumerable<GetCinemasQueryResponse>>
    {
    }
}
