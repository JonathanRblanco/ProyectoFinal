using Ardalis.Result;
using MediatR;

namespace ProyectoFinal.DTO.Requests
{
    public class ModifyGenderRequest : IRequest<Result>
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
