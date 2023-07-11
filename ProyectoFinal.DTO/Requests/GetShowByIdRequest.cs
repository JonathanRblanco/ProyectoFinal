using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Responses;

namespace ProyectoFinal.DTO.Requests
{
    public class GetShowByIdRequest : IRequest<Result<GetShowByIdResponse>>
    {
        public int Id { get; set; }
    }
}
