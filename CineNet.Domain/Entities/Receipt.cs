namespace CineNet.Domain.Entities
{
    public class Receipt
    {
        public int Id { get; set; }
        public int ShowId { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public string Code { get; set; }
        public decimal Total { get; set; }
        public int AmountOfTickets { get; set; }
        public Show Show { get; set; }
    }
}
