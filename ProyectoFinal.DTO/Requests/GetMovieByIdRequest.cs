using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Responses;


namespace ProyectoFinal.DTO.Requests
{
    public class GetMovieByIdRequest : IRequest<Result<GetMovieByIdResponse>>
    {
        public int Id { get; set; }
    }
}
