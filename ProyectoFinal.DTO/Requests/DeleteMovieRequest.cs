using Ardalis.Result;
using MediatR;

namespace ProyectoFinal.DTO.Requests
{
    public class DeleteMovieRequest : IRequest<Result>
    {
        public int id { get; set; }
    }
}
