using Ardalis.Result;
using MediatR;

namespace ProyectoFinal.DTO.Requests
{
    public class ModifyMovieRequest : IRequest<Result>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Synopsis { get; set; }
        public int Duration { get; set; }
        public int GenderId { get; set; }
        public int ClasificationId { get; set; }
        public string Actors { get; set; }
        public string Director { get; set; }
        public int ImageId { get; set; }
    }
}
