using MediatR;

namespace CineNet.Aplication.Queries
{
    public class GetImageQuery : IRequest<GetImageQueryResponse>
    {
        public int Id { get; set; }
    }
}
