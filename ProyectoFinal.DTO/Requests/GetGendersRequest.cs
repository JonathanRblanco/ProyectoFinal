using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Responses;

namespace ProyectoFinal.DTO.Requests
{
    public class GetGendersRequest : IRequest<Result<IEnumerable<GetGendersResponse>>>
    {
    }
}
