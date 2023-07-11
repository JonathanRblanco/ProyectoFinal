using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Hosting;
using ProyectoFinal.Data;
using ProyectoFinal.Data.Email;
using ProyectoFinal.ServicesContracts;
using System.Text.Json;

namespace ProyectoFinal.Services
{
    public class ChangePasswordEmailTaskService : ITasksService
    {
        private readonly IApiService apiService;
        private readonly UserManager<User> userManager;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly LinkGenerator linkGenerator;
        private readonly IViewRenderService viewRenderService;
        private readonly IEmailService emailService;

        public ChangePasswordEmailTaskService(IApiService apiService,
            UserManager<User> userManager,
            IWebHostEnvironment webHostEnvironment,
            LinkGenerator linkGenerator,
            IViewRenderService viewRenderService,
            IEmailService emailService)
        {
            this.apiService = apiService;
            this.userManager = userManager;
            this.webHostEnvironment = webHostEnvironment;
            this.linkGenerator = linkGenerator;
            this.viewRenderService = viewRenderService;
            this.emailService = emailService;
        }
        private async Task<string> GetAll()
        {
            return await apiService.GetAsync("Tasks/Emails/ChangePassword");
        }
        private async Task<T> GetAll<T>()
        {
            var response = await this.GetAll();
            return JsonSerializer.Deserialize<T>(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
        private async Task<string> Delete(int id)
        {
            return await apiService.DeleteAsync("Tasks/Emails/" + id);
        }
        public async Task ExecuteTask()
        {
            var tasks = await this.GetAll<IEnumerable<EmailTask>>();
            foreach (var task in tasks)
            {
                var data = JsonSerializer.Deserialize<ChangePasswordTaskModel>(task.Data);
                var user = await userManager.FindByIdAsync(data.UserId);
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = linkGenerator.GetPathByAction("ChangePassword", "Account", new { UserId = user.Id, Token = token });
                var appUrl = webHostEnvironment.IsDevelopment() ? "https://localhost:5001" : "https://cinenet.bsite.net";
                var absoluteUrl = appUrl + callbackUrl;
                var model = new ChangePasswordModel
                {
                    Url = absoluteUrl,
                    UserName = user.UserName
                };
                var view = await viewRenderService.RenderToString("ChangePassword", model);
                var emailModel = new EmailInfo("Recuperar contraseña", view, new List<string> { user.Email })
                {
                    IsBodyHtml = true,
                };
                await emailService.SendEmail(emailModel);
                await this.Delete(task.Id);
            }
        }
    }
}
