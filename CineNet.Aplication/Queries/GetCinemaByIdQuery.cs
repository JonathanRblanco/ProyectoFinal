using MediatR;

namespace CineNet.Aplication.Queries
{
    public class GetCinemaByIdQuery : IRequest<GetCinemaByIdQueryResponse>
    {
        public int Id { get; set; }
    }
}
