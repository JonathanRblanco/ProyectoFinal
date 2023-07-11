namespace CineNet.Domain.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Synopsis { get; set; }
        public int GenderId { get; set; }
        public int Duration { get; set; }
        public int ClasificationId { get; set; }
        public string Actors { get; set; }
        public string Director { get; set; }
        public int ImageId { get; set; }
        public Gender Gender { get; set; }
        public Clasification Clasification { get; set; }
    }
}
