namespace ProyectoFinal.DTO.Responses
{
    public class GetMovieByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Synopsis { get; set; }
        public int GenderId { get; set; }
        public int Duration { get; set; }
        public string Actors { get; set; }
        public string Director { get; set; }
        public int ClasificationId { get; set; }
        public int ImageId { get; set; }
        public GetGenderByIdReponse Gender { get; set; }
        public GetClasificationByIdResponse Clasification { get; set; }
    }
}
