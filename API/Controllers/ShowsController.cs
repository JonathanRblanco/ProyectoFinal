using CineNet.Aplication.Commands;
using CineNet.Aplication.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShowsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ShowsController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.OK, Type = typeof(IEnumerable<GetShowsQueryResponse>))]
        public async Task<ActionResult<IEnumerable<GetShowsQueryResponse>>> Get() => Ok(await mediator.Send(new GetShowsQuery()));
        [HttpGet("{id}")]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.OK, Type = typeof(GetShowByIdQueryResponse))]
        public async Task<ActionResult<GetShowByIdQueryResponse>> Get(int id) => Ok(await mediator.Send(new GetShowByIdQuery() { Id = id }));
        [HttpPost]
        public async Task<ActionResult<CreateShowCommandResponse>> Post(CreateShowCommand command) => Ok(await mediator.Send(command));
        [HttpPut]
        public async Task<ActionResult<UpdateShowCommandResponse>> Put(UpdateShowCommand command) => Ok(await mediator.Send(command));
        [HttpDelete("{showId}")]
        public async Task<ActionResult<DeleteShowCommandResponse>> Delete(int showId) => Ok(await mediator.Send(new DeleteShowCommand { Id = showId }));
    }
}
