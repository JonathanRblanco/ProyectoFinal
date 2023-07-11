using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ProyectoFinal.Data;
using ProyectoFinal.Data.Email;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.ServicesContracts;
using System.Text.Json;

namespace ProyectoFinal.DTO.Handlers.UserHanlders
{
    public class ReSendReceiptEmailHandler : IRequestHandler<ReSendReceiptEmailRequest, Result>
    {
        private readonly UserManager<User> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IEmailTasksService emailTasksService;

        public ReSendReceiptEmailHandler(UserManager<User> userManager,
            IHttpContextAccessor httpContextAccessor,
            IEmailTasksService emailTasksService)
        {
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
            this.emailTasksService = emailTasksService;
        }
        public async Task<Result> Handle(ReSendReceiptEmailRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await userManager.GetUserAsync(httpContextAccessor.HttpContext.User);
                if (user == null)
                {
                    return Result.Error("Usuario invalido");
                }
                var emailTask = new EmailTask
                {
                    Data = JsonSerializer.Serialize(new { ReceiptId = request.ReceiptId, UserId = user.Id }),
                    Type = "SendReceipt"
                };
                await emailTasksService.CreateTask(JsonSerializer.Serialize(emailTask));
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message);
            }
        }
    }
}
