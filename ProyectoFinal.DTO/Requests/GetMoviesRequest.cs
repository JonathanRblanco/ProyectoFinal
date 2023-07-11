using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Responses;

namespace ProyectoFinal.DTO.Requests
{
    public class GetMoviesRequest : IRequest<Result<IEnumerable<GetMoviesResponse>>>
    {
        public bool OnlyWithShows { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
    }
}
