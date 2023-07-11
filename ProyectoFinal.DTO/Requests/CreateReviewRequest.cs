using Ardalis.Result;
using MediatR;

namespace ProyectoFinal.DTO.Requests
{
    public class CreateReviewRequest : IRequest<Result>
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string NormalizedUserName { get; set; }
        public string UserId { get; set; }
        public int MovieId { get; set; }
        public DateTime Date { get; set; }
    }
}
