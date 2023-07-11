using MediatR;
using Microsoft.AspNetCore.Http;

namespace CineNet.Aplication.Commands
{
    public class CreateMovieCommand : IRequest<CreateMovieCommandResponse>
    {
        public string Name { get; set; }

        public string Synopsis { get; set; }

        public int GenderId { get; set; }

        public string Duration { get; set; }

        public int ClasificationId { get; set; }

        public string Actors { get; set; }

        public string Director { get; set; }
        public IFormFile Image { get; set; }
    }

}
