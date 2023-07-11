using Ardalis.Result;
using MediatR;

namespace ProyectoFinal.DTO.Requests
{
    public class DeleteShowRequest : IRequest<Result>
    {
        public int Id { get; set; }
    }
}
