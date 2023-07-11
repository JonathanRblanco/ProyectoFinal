using Ardalis.Result;
using MediatR;

namespace ProyectoFinal.DTO.Requests
{
    public class CreateGenderRequest : IRequest<Result>
    {
        public string Description { get; set; }
    }
}
