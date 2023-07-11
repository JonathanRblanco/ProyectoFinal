namespace CineNet.Domain.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public int MovieId { get; set; }
        public DateTime Date { get; set; }
        public Movie Movie { get; set; }
    }
}
