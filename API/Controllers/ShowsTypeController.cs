using CineNet.Aplication.Commands;
using CineNet.Aplication.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShowsTypeController : ControllerBase
    {
        private readonly IMediator mediator;

        public ShowsTypeController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.OK, Type = typeof(IEnumerable<GetShowsTypeQueryResponse>))]
        public async Task<ActionResult<IEnumerable<GetShowsTypeQueryResponse>>> Get() => Ok(await mediator.Send(new GetShowsTypeQuery()));
        [HttpGet("{id}")]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.OK, Type = typeof(GetShowTypeByIdQueryResponse))]
        public async Task<ActionResult<GetShowTypeByIdQueryResponse>> Get(int id) => Ok(await mediator.Send(new GetShowTypeByIdQuery() { Id = id }));
        [HttpPost]
        public async Task<ActionResult<CreateShowTypeCommandResponse>> Post(CreateShowTypeCommand command) => Ok(await mediator.Send(command));
        [HttpPut]
        public async Task<ActionResult<UpdateShowTypeCommandResponse>> Put(UpdateShowTypeCommand command) => Ok(await mediator.Send(command));
        [HttpDelete("{showTypeId}")]
        public async Task<ActionResult<DeleteShowTypeCommandResponse>> Delete(int showTypeId) => Ok(await mediator.Send(new DeleteShowTypeCommand { Id = showTypeId }));
    }
}
