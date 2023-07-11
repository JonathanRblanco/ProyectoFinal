namespace ProyectoFinal.DTO.Responses
{
    public class GetShowByIdResponse
    {
        public int Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public int ShowTypeId { get; set; }
        public int MovieId { get; set; }
        public int RoomId { get; set; }
        public int Capacity { get; set; }
        public decimal Price { get; set; }
        public GetMoviesResponse Movie { get; set; }
        public GetRoomByIdResponse Room { get; set; }
        public GetShowsTypeByIdResponse ShowType { get; set; }
    }
}
