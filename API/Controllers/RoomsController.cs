using CineNet.Aplication.Commands;
using CineNet.Aplication.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoomsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.OK, Type = typeof(IEnumerable<GetRoomsQueryResponse>))]
        public async Task<ActionResult<IEnumerable<GetRoomsQueryResponse>>> Get() => Ok(await _mediator.Send(new GetRoomsQuery()));
        [HttpGet("{id}")]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.OK, Type = typeof(GetRoomByIdQueryResponse))]
        public async Task<ActionResult<GetRoomByIdQueryResponse>> Get(int id) => Ok(await _mediator.Send(new GetRoomByIdQuery() { Id = id }));
        [HttpPost]
        public async Task<ActionResult<CreateRoomCommandResponse>> Post(CreateRoomCommand command) => Ok(await _mediator.Send(command));
        [HttpPut]
        public async Task<ActionResult<UpdateRoomCommandResponse>> Put(UpdateRoomCommand command) => Ok(await _mediator.Send(command));
        [HttpDelete("{roomId}")]
        public async Task<ActionResult<DeleteRoomCommandResponse>> Delete(int roomId) => Ok(await _mediator.Send(new DeleteRoomCommand { Id = roomId }));
    }
}
