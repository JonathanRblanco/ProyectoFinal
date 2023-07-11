namespace CineNet.Domain.Entities
{
    public class Show
    {
        public int Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public int Capacity { get; set; }
        public int ShowTypeId { get; set; }
        public int MovieId { get; set; }
        public int RoomId { get; set; }
        public decimal Price { get; set; }
        public ShowType ShowType { get; set; }
        public Movie Movie { get; set; }
        public Room Room { get; set; }
    }
}
