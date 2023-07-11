using Ardalis.Result;
using MediatR;

namespace ProyectoFinal.DTO.Requests
{
    public class CreateClasificationRequest : IRequest<Result>
    {
        public string Description { get; set; }
    }
}
