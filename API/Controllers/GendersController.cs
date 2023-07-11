using CineNet.Aplication.Commands;
using CineNet.Aplication.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GendersController : ControllerBase
    {
        private readonly IMediator mediator;

        public GendersController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.OK, Type = typeof(IEnumerable<GetGendersQueryResponse>))]
        public async Task<ActionResult<IEnumerable<GetGendersQueryResponse>>> Get() => Ok(await mediator.Send(new GetGendersQuery()));
        [HttpGet("{id}")]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.OK, Type = typeof(GetGenderByIdQueryResponse))]
        public async Task<ActionResult<GetGenderByIdQueryResponse>> Get(int id) => Ok(await mediator.Send(new GetGenderByIdQuery() { Id = id }));

        [HttpPost]
        public async Task<ActionResult<CreateGenderCommandResponse>> Post(CreateGenderCommand command) => Ok(await mediator.Send(command));
        [HttpPut]
        public async Task<ActionResult<UpdateGenderCommandResponse>> Put(UpdateGenderCommand command) => Ok(await mediator.Send(command));
        [HttpDelete("{genderId}")]
        public async Task<ActionResult<DeleteGenderCommand>> Delete(int genderId) => Ok(await mediator.Send(new DeleteGenderCommand { Id = genderId }));
    }
}
