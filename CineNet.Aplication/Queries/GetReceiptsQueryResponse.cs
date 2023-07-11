namespace CineNet.Aplication.Queries
{
    public class GetReceiptsQueryResponse
    {
        public int Id { get; set; }
        public int ShowId { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public string Code { get; set; }
        public decimal Total { get; set; }
        public int AmountOfTickets { get; set; }
        public GetShowsQueryResponse Show { get; set; }
    }
}
