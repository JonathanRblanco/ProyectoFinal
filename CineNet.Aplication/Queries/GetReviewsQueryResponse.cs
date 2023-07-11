namespace CineNet.Aplication.Queries
{
    public class GetReviewsQueryResponse
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public int MovieId { get; set; }
        public DateTime Date { get; set; }
        public GetMoviesQueryResponse Movie { get; set; }
    }
}
