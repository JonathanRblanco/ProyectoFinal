using MediatR;
using ProyectoFinal.DTO.Responses;

namespace ProyectoFinal.DTO.Requests
{
    public class GetUserByIdRequest : IRequest<GetUserByIdResponse>
    {
        public string Id { get; set; }
    }
}
