using Ardalis.Result;
using MediatR;

namespace ProyectoFinal.DTO.Requests
{
    public class ReSendReceiptEmailRequest : IRequest<Result>
    {
        public int ReceiptId { get; set; }
    }
}
