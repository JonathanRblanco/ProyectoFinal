using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Responses;

namespace ProyectoFinal.DTO.Requests
{
    public class GetShowTypeByIdRequest : IRequest<Result<GetShowsTypeByIdResponse>>
    {
        public int Id { get; set; }
    }
}
