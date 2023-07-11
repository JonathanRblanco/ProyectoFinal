using Ardalis.Result;
using MediatR;

namespace ProyectoFinal.DTO.Requests
{
    public class DeleteClasificationRequest : IRequest<Result>
    {
        public int Id { get; set; }
    }
}
