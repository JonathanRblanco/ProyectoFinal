using CineNet.Aplication.Commands;
using CineNet.Aplication.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMediator mediator;

        public MoviesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.OK, Type = typeof(IEnumerable<GetMoviesQueryResponse>))]
        public async Task<ActionResult<IEnumerable<GetMoviesQueryResponse>>> Get() => Ok(await mediator.Send(new GetMoviesQuery()));

        [HttpPost]
        public async Task<ActionResult<CreateMovieCommand>> Post([FromForm] CreateMovieCommand command) => Ok(await mediator.Send(command));

        [HttpDelete("{movieId}")]
        public async Task<ActionResult<DeleteMovieCommand>> Delete(int movieId) => Ok(await mediator.Send(new DeleteMovieCommand { id = movieId }));

        [HttpPut]
        public async Task<ActionResult<UpdateMovieCommandResponse>> Put(UpdateMovieCommand command) => Ok(await mediator.Send(command));

        [HttpGet("{id}")]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.OK, Type = typeof(GetMovieByIdQueryResponse))]
        public async Task<ActionResult<GetMovieByIdQueryResponse>> Get(int id) => Ok(await mediator.Send(new GetMovieByIdQuery() { Id = id }));
    }
}
