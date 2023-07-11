using CineNet.Aplication.Commands;
using CineNet.Aplication.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IMediator mediator;

        public TasksController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet("Emails/Receipts")]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.OK, Type = typeof(IEnumerable<GetEmailReceiptTasksQueryResponse>))]
        public async Task<ActionResult<IEnumerable<GetEmailReceiptTasksQueryResponse>>> GetReceiptsTask() => Ok(await mediator.Send(new GetEmailReceiptTasksQuery()));
        [HttpGet("Emails/ConfirmEmail")]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.OK, Type = typeof(IEnumerable<GetEmailReceiptTasksQueryResponse>))]
        public async Task<ActionResult<IEnumerable<GetConfirmEmailTasksQueryResponse>>> GetConfirmEmailTasks() => Ok(await mediator.Send(new GetConfirmEmailTasksQuery()));
        [HttpGet("Emails/ChangePassword")]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.OK, Type = typeof(IEnumerable<GetEmailReceiptTasksQueryResponse>))]
        public async Task<ActionResult<IEnumerable<GetChangePasswordTasksQueryResponse>>> GetChangePasswordTasks() => Ok(await mediator.Send(new GetChangePasswordTasksQuery()));

        [HttpDelete("Emails/{taskId}")]
        public async Task<ActionResult<DeleteTaskCommandResponse>> Delete(int taskId) => Ok(await mediator.Send(new DeleteTaskCommand { Id = taskId }));
        [HttpPost("Emails")]
        public async Task<ActionResult<CreateEmailTaskCommandResponse>> Post(CreateEmailTaskCommand command) => Ok(await mediator.Send(command));
    }
}
