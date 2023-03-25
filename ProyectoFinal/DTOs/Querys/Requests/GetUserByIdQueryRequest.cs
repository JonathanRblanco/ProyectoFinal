using MediatR;
using ProyectoFinal.DTOs.Querys.Responses;

namespace ProyectoFinal.DTOs.Querys.Requests
{
    public class GetUserByIdQueryRequest : IRequest<GetUserByIdQueryResponse>
    {
        public string Id { get; set; }
    }
}
