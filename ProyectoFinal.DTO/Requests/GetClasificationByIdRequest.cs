using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Responses;

namespace ProyectoFinal.DTO.Requests
{
    public class GetClasificationByIdRequest : IRequest<Result<GetClasificationByIdResponse>>
    {
        public int Id { get; set; }
    }
}
