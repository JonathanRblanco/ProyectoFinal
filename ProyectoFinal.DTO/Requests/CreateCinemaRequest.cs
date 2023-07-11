using Ardalis.Result;
using MediatR;


namespace ProyectoFinal.DTO.Requests
{
    public class CreateCinemaRequest : IRequest<Result>
    {
        public string Name { get; set; }
        public string URL { get; set; }
    }
}
