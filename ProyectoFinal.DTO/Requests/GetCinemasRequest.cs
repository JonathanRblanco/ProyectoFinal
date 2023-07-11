using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Responses;

namespace ProyectoFinal.DTO.Requests
{
    public class GetCinemasRequest : IRequest<Result<IEnumerable<GetCinemasResponse>>>
    {
        public int Id { get; set; }
        public bool ByUser { get; set; }
    }
}
