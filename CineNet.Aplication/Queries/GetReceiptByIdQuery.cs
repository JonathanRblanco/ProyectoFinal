using MediatR;

namespace CineNet.Aplication.Queries
{
    public class GetReceiptByIdQuery : IRequest<GetReceiptByIdQueryResponse>
    {
        public int Id { get; set; }
    }
}
