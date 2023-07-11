using Ardalis.Result;
using MediatR;

namespace ProyectoFinal.DTO.Requests
{
    public class ReceiptPdfRequest : IRequest<Result<byte[]>>
    {
        public string Address { get; set; }
        public string Movie { get; set; }
        public string ShowType { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Room { get; set; }
        public string AmountOfTickets { get; set; }
        public string Total { get; set; }
        public string Code { get; set; }
        public string BranchName { get; set; }
    }
}
