using CineNet.Aplication.Commands;
using CineNet.Aplication.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly IMediator mediator;

        public ImagesController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet("{id}")]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.OK, Type = typeof(GetImageQueryResponse))]
        public async Task<ActionResult<GetImageQueryResponse>> GetById(int id) => Ok(await mediator.Send(new GetImageQuery { Id = id }));
        [HttpPost]
        public async Task<ActionResult<int>> Post(IFormFile file)
        {
            var result = await mediator.Send(new CreateImageCommand { File = file });
            return result.Id;
        }
    }
}
