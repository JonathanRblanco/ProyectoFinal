using MediatR;

namespace CineNet.Aplication.Commands
{
    public class CreateReviewCommand : IRequest<CreateReviewCommandResponse>
    {
        public string Description { get; set; }
        public string UserId { get; set; }
        public int MovieId { get; set; }
        public DateTime Date { get; set; }
    }
}
