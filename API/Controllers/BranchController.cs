using CineNet.Aplication.Commands;
using CineNet.Aplication.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BranchController : ControllerBase
    {
        private readonly IMediator mediator;

        public BranchController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.OK, Type = typeof(IEnumerable<GetBranchesQueryResponse>))]
        public async Task<ActionResult<IEnumerable<GetBranchesQueryResponse>>> Get() => Ok(await mediator.Send(new GetBranchesQuery()));

        [HttpPost]
        public async Task<ActionResult<CreateBranchCommand>> Post(CreateBranchCommand command) => Ok(await mediator.Send(command));
        [HttpGet("{id}")]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.OK, Type = typeof(GetBranchByIdQueryResponse))]
        public async Task<ActionResult<GetBranchByIdQueryResponse>> Get(int id) => Ok(await mediator.Send(new GetBranchByIdQuery() { Id = id }));
        [HttpPut]
        public async Task<ActionResult<UpdateBranchCommandResponse>> Put(UpdateBranchCommand command) => Ok(await mediator.Send(command));

        [HttpDelete("{branchId}")]
        public async Task<ActionResult<DeleteBranchCommandResponse>> Delete(int branchId) => Ok(await mediator.Send(new DeleteBranchCommand { Id = branchId }));

    }
}
