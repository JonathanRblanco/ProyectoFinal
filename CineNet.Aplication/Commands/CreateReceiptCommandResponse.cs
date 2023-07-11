using CineNet.Aplication.Queries;

namespace CineNet.Aplication.Commands
{
    public class CreateReceiptCommandResponse
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Code { get; set; }
        public decimal Total { get; set; }
        public int AmountOfTickets { get; set; }
        public GetShowsQueryResponse Show { get; set; }
    }
}
