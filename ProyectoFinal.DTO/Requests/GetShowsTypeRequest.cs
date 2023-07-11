using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Responses;

namespace ProyectoFinal.DTO.Requests
{
    public class GetShowsTypeRequest : IRequest<Result<IEnumerable<GetShowsTypeResponse>>>
    {
    }
}
