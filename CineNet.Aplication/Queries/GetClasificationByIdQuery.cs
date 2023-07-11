using MediatR;

namespace CineNet.Aplication.Queries
{
    public class GetClasificationByIdQuery : IRequest<GetClasificationByIdQueryResponse>
    {
        public int Id { get; set; }
    }
}
