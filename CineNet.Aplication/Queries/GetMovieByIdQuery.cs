using MediatR;

namespace CineNet.Aplication.Queries
{
    public class GetMovieByIdQuery : IRequest<GetMovieByIdQueryResponse>
    {
        public int Id { get; set; }
    }
}
