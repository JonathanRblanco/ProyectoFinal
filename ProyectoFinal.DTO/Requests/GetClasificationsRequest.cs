using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Responses;

namespace ProyectoFinal.DTO.Requests
{
    public class GetClasificationsRequest : IRequest<Result<IEnumerable<GetClasificationsResponse>>>
    {
    }
}
