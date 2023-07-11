using CineNet.Aplication.Commands;
using CineNet.Aplication.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReceiptsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ReceiptsController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.OK, Type = typeof(IEnumerable<GetMoviesQueryResponse>))]
        public async Task<ActionResult<IEnumerable<GetReceiptsQueryResponse>>> Get() => Ok(await mediator.Send(new GetReceiptsQuery()));
        [HttpPost]
        public async Task<ActionResult<CreateReceiptCommandResponse>> Post(CreateReceiptCommand command) => await mediator.Send(command);
        [HttpGet("{id}")]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.OK, Type = typeof(GetReceiptByIdQueryResponse))]
        public async Task<ActionResult<GetReceiptByIdQueryResponse>> Get(int id) => Ok(await mediator.Send(new GetReceiptByIdQuery() { Id = id }));
    }
}
