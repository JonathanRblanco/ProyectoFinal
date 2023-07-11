using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Responses;

namespace ProyectoFinal.DTO.Requests
{
    public class GetReviewsRequest : IRequest<Result<IEnumerable<GetReviewsResponse>>>
    {
        public string UserId { get; set; }
        public int MovieId { get; set; }
        public DateTime Date { get; set; }
    }
}
