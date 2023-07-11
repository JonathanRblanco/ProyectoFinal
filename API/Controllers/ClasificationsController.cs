using CineNet.Aplication.Commands;
using CineNet.Aplication.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClasificationsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ClasificationsController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.OK, Type = typeof(IEnumerable<GetClasificationsQueryResponse>))]
        public async Task<ActionResult<IEnumerable<GetClasificationsQueryResponse>>> Get() => Ok(await mediator.Send(new GetClasificationsQuery()));
        [HttpGet("{id}")]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.OK, Type = typeof(GetClasificationByIdQueryResponse))]
        public async Task<ActionResult<GetClasificationByIdQueryResponse>> Get(int id) => Ok(await mediator.Send(new GetClasificationByIdQuery() { Id = id }));
        [HttpPost]
        public async Task<ActionResult<CreateClasificationCommandResponse>> Post(CreateClasificationCommand command) => Ok(await mediator.Send(command));
        [HttpPut]
        public async Task<ActionResult<UpdateClasificationCommandResponse>> Put(UpdateClasificationCommand command) => Ok(await mediator.Send(command));
        [HttpDelete("{clasificationId}")]
        public async Task<ActionResult<DeleteClasificationCommandResponse>> Delete(int clasificationId) => Ok(await mediator.Send(new DeleteClasificationCommand { Id = clasificationId }));
    }
}
