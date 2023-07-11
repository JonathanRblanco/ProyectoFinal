using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Responses;

namespace ProyectoFinal.DTO.Requests
{
    public class GetShowsRequest : IRequest<Result<IEnumerable<GetShowsResponse>>>
    {
        public int BranchId { get; set; }
        public int MovieId { get; set; }
        public bool OnlyValid { get; set; }
        public int? CinemaId { get; set; }
    }
}
