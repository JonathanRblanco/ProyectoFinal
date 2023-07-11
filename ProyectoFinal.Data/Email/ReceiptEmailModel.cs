namespace ProyectoFinal.Data.Email
{
    public class ReceiptEmailModel
    {
        public string UserName { get; set; }
        public string MovieName { get; set; }
        public int Room { get; set; }
        public DateTimeOffset Date { get; set; }
        public int AmountOfTickets { get; set; }
        public string BranchCinemaName { get; set; }
        public string Address { get; set; }
        public string Code { get; set; }
        public decimal Total { get; set; }
        public string BranchPhone { get; set; }
        public string BranchEmail { get; set; }
    }
}
