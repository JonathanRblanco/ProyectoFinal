using Ardalis.Result;
using MediatR;

namespace ProyectoFinal.DTO.Requests
{
    public class ExternalLoginRequest : IRequest<Result>
    {
        public string ReturnUrl { get; set; }
        public string RemoteError { get; set; }
    }
}
