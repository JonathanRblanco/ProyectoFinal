using MediatR;

namespace CineNet.Aplication.Queries
{
    public class GetGenderByIdQuery : IRequest<GetGenderByIdQueryResponse>
    {
        public int Id { get; set; }
    }
}
