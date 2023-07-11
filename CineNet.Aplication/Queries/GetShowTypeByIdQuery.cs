using MediatR;

namespace CineNet.Aplication.Queries
{
    public class GetShowTypeByIdQuery : IRequest<GetShowTypeByIdQueryResponse>
    {
        public int Id { get; set; }
    }
}
