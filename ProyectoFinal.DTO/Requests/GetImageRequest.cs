using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Responses;

namespace ProyectoFinal.DTO.Requests
{
    public class GetImageRequest : IRequest<Result<GetImageResponse>>
    {
        public int Id { get; set; }
    }
}
