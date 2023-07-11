using Ardalis.Result;
using MediatR;

namespace ProyectoFinal.DTO.Requests
{
    public class DeleteShowTypeRequest : IRequest<Result>
    {
        public int Id { get; set; }
    }
}
