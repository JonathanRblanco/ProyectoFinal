using Ardalis.Result;
using MediatR;

namespace ProyectoFinal.DTO.Requests
{
    public class PaymentRequest : IRequest<Result>
    {
        public int ShowId { get; set; }
        public string UserId { get; set; }
        public int AmountOfTickets { get; set; }
        public decimal Total { get; set; }
    }
}
