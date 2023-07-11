using MediatR;

namespace CineNet.Aplication.Queries
{
    public class GetRoomByIdQuery : IRequest<GetRoomByIdQueryResponse>
    {
        public int Id { get; set; }
    }
}
