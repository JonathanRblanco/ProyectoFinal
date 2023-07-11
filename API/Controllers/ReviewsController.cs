using CineNet.Aplication.Commands;
using CineNet.Aplication.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly IMediator mediator;
        public ReviewsController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.OK, Type = typeof(IEnumerable<GetReviewsQueryResponse>))]
        public async Task<ActionResult<IEnumerable<GetReviewsQueryResponse>>> Get() => Ok(await mediator.Send(new GetReviewsQuery()));
        [HttpPost]
        public async Task<ActionResult<CreateReviewCommand>> Post(CreateReviewCommand command) => Ok(await mediator.Send(command));
    }
}
