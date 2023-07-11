using Ardalis.Result;
using MediatR;

namespace ProyectoFinal.DTO.Requests
{
    public class CreateRolRequest : IRequest<Result>
    {
        public string Name { get; set; }
    }
}
