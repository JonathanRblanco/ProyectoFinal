using Ardalis.Result;
using MediatR;

namespace ProyectoFinal.DTO.Requests
{
    public class DeleteCinemaRequest : IRequest<Result>
    {
        public int Id { get; set; }
    }
}
