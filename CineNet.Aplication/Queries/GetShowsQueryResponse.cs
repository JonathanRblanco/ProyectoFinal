namespace CineNet.Aplication.Queries
{
    public class GetShowsQueryResponse
    {
        public int Id { get; set; }
        public int ShowTypeId { get; set; }
        public DateTimeOffset Date { get; set; }
        public int MovieId { get; set; }
        public int RoomId { get; set; }
        public int Capacity { get; set; }
        public decimal Price { get; set; }
        public GetMoviesQueryResponse Movie { get; set; }
        public GetRoomsQueryResponse Room { get; set; }
        public GetShowsTypeQueryResponse ShowType { get; set; }
    }
}
