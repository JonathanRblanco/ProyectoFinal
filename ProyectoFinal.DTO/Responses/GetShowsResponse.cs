namespace ProyectoFinal.DTO.Responses
{
    public class GetShowsResponse
    {
        public int Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public int ShowTypeId { get; set; }
        public int MovieId { get; set; }
        public int RoomId { get; set; }
        public int Capacity { get; set; }
        public decimal Price { get; set; }
        public GetMoviesResponse Movie { get; set; }
        public GetRoomsResponse Room { get; set; }
        public GetShowsTypeResponse ShowType { get; set; }
    }
}
