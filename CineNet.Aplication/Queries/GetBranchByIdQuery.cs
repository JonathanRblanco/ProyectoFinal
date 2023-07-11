using MediatR;

namespace CineNet.Aplication.Queries
{
    public class GetBranchByIdQuery : IRequest<GetBranchByIdQueryResponse>
    {
        public int Id { get; set; }
    }
}
