namespace ProyectoFinal.DTO.Responses
{
    public class GetReviewsResponse
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        public int MovieId { get; set; }
        public DateTime Date { get; set; }

    }
}
