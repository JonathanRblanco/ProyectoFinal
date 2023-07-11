using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ProyectoFinal.DTO.Requests
{
    public class ChangeImageRequest : IRequest<Result<byte[]>>
    {
        public IFormFile image { get; set; }
        public string UserId { get; set; }
    }
}
