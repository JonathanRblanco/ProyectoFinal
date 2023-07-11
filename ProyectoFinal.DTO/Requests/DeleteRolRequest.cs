using Ardalis.Result;
using MediatR;

namespace ProyectoFinal.DTO.Requests
{
    public class DeleteRolRequest : IRequest<Result>
    {
        public string Id { get; set; }
    }
}
