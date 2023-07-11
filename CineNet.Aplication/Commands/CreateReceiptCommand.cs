using MediatR;

namespace CineNet.Aplication.Commands
{
    public class CreateReceiptCommand : IRequest<CreateReceiptCommandResponse>
    {
        public int ShowId { get; set; }
        public string UserId { get; set; }
        public int AmountOfTickets { get; set; }
        public decimal Total { get; set; }
    }
}
