using MediatR;
using Microsoft.AspNetCore.Http;

namespace CineNet.Aplication.Commands
{
    public class CreateImageCommand : IRequest<CreateImageCommandResponse>
    {
        public IFormFile File { get; set; }
    }
}
