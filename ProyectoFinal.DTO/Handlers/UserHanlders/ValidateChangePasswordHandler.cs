using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProyectoFinal.Data;
using ProyectoFinal.Data.Email;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.ServicesContracts;
using System.Text.Json;

namespace ProyectoFinal.DTO.Handlers.UserHanlders
{
    public class ValidateChangePasswordHandler : IRequestHandler<ValidateChangePasswordRequest, Result>
    {
        private readonly UserManager<User> userManager;
        private readonly IEmailTasksService emailTasksService;

        public ValidateChangePasswordHandler(UserManager<User> userManager,
            IEmailTasksService emailTasksService)
        {
            this.userManager = userManager;
            this.emailTasksService = emailTasksService;
        }
        public async Task<Result> Handle(ValidateChangePasswordRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.UserEmail))
                {
                    return Result.Error("La dirección de correo electronico es obligatoria");
                }
                var user = await userManager.FindByEmailAsync(request.UserEmail);
                if (user == null)
                {
                    return Result.Error("No existe ningun usuario registrado con la dirección de correo proporcionada");
                }
                var emailTask = new EmailTask
                {
                    Data = JsonSerializer.Serialize(new { UserId = user.Id }),
                    Type = "ChangePassword"
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
