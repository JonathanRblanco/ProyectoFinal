using Ardalis.Result;
using MediatR;

namespace ProyectoFinal.DTO.Requests
{
    public class ModifyRolRequest : IRequest<Result>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName => Name.ToUpper();
    }
}
