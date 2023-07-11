using MediatR;

namespace CineNet.Aplication.Queries
{
    public class GetShowByIdQuery : IRequest<GetShowByIdQueryResponse>
    {
        public int Id { get; set; }
    }
}
