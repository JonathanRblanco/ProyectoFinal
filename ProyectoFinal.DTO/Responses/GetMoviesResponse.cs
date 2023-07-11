namespace ProyectoFinal.DTO.Responses
{
    public class GetMoviesResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public GetGendersResponse Gender { get; set; }
        public int Duration { get; set; }
        public GetClasificationsResponse Clasification { get; set; }
        public int ImageId { get; set; }
    }
}
