namespace ProyectoFinal.DTO.Responses
{
    public class GetPaymentsResponse
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public decimal Total { get; set; }
        public DateTime Date { get; set; }
        public string Code { get; set; }
        public int AmountOfTickets { get; set; }
        public GetShowsResponse Show { get; set; }
    }
}
