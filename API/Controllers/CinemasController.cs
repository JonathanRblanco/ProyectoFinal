using CineNet.Aplication.Commands;
using CineNet.Aplication.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CinemasController : ControllerBase
    {
        private readonly IMediator mediator;

        public CinemasController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.OK, Type = typeof(IEnumerable<GetCinemasQueryResponse>))]
        public async Task<ActionResult<IEnumerable<GetCinemasQueryResponse>>> Get() => Ok(await mediator.Send(new GetCinemasQuery()));

        [HttpGet("Statistics")]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.OK, Type = typeof(IEnumerable<GetStatisticsQueryResponse>))]
        public async Task<ActionResult<IEnumerable<GetStatisticsQueryResponse>>> GetStatistics([FromQuery] GetStatisticsQuery query) => Ok(await mediator.Send(query));

        [HttpGet("{id}")]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.OK, Type = typeof(GetCinemaByIdQueryResponse))]
        public async Task<ActionResult<GetCinemaByIdQueryResponse>> Get(int id) => Ok(await mediator.Send(new GetCinemaByIdQuery() { Id = id }));

        [HttpGet("user/{userId}/cinemas")]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.OK, Type = typeof(IEnumerable<GetCinemasByUserIdQueryResponse>))]
        public async Task<ActionResult<IEnumerable<GetCinemasByUserIdQueryResponse>>> GetByUserId(string userId) => Ok(await mediator.Send(new GetCinemasByUserIdQuery() { UserId = userId }));

        [HttpPost]
        public async Task<ActionResult<CreateCinemaCommand>> Post(CreateCinemaCommand command) => Ok(await mediator.Send(command));
        [HttpPut]
        public async Task<ActionResult<UpdateCinemaCommandResponse>> Put(UpdateCinemaCommand command) => Ok(await mediator.Send(command));
        [HttpDelete("{cinemaId}")]
        public async Task<ActionResult<DeleteCinemaCommand>> Delete(int cinemaId) => Ok(await mediator.Send(new DeleteCinemaCommand { Id = cinemaId }));

        [HttpPost("assign")]
        public async Task<ActionResult<AssignCinemaByUserIdCommandResponse>> Assign(AssignCinemaByUserIdCommand command) => Ok(await mediator.Send(command));

    }
}