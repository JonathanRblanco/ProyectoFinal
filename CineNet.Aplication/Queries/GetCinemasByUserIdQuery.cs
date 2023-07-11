using MediatR;

namespace CineNet.Aplication.Queries
{
    public class GetCinemasByUserIdQuery : IRequest<IEnumerable<GetCinemasByUserIdQueryResponse>>
    {
        public string UserId { get; set; }
    }
}
