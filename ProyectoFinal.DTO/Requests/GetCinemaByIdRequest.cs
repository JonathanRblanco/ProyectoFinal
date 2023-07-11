using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Responses;

namespace ProyectoFinal.DTO.Requests
{
    public class GetCinemaByIdRequest : IRequest<Result<GetCinemaByIdResponse>>
    {
        public int Id { get; set; }
    }
}
