using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Responses;

namespace ProyectoFinal.DTO.Requests
{
    public class GetBranchesRequest : IRequest<Result<IEnumerable<GetBranchesResponse>>>
    {
        public int? CinemaId { get; set; }
    }
}
