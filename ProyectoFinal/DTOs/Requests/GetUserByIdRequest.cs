using MediatR;
using ProyectoFinal.DTOs.Responses;

namespace ProyectoFinal.DTOs.Requests
{
    public class GetUserByIdQueryRequest : IRequest<GetUserByIdResponse>
    {
        public string Id { get; set; }
    }
}
