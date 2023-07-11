using Ardalis.Result;
using MediatR;

namespace ProyectoFinal.DTO.Requests
{
    public class ModifyAssignCinemaRequest : IRequest<Result>
    {
        public List<int> Ids { get; set; } = new List<int>();
        public string UserId { get; set; }
    }
}
