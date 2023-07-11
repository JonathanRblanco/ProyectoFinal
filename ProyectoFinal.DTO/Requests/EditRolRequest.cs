using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Responses;

namespace ProyectoFinal.DTO.Requests
{
    public class EditRolRequest : IRequest<Result<EditRolResponse>>
    {
        public string Id { get; set; }
    }
}
