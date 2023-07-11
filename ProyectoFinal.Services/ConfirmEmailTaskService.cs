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
    public class ConfirmEmailTaskService : ITasksService
    {
        private readonly IApiService apiService;
        private readonly IViewRenderService viewRenderService;
        private readonly UserManager<User> userManager;
        private readonly IEmailService emailService;
        private readonly LinkGenerator linkGenerator;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ConfirmEmailTaskService(IApiService apiService,
            IViewRenderService viewRenderService,
            UserManager<User> userManager,
            IEmailService emailService,
            LinkGenerator linkGenerator,
            IWebHostEnvironment webHostEnvironment)
        {
            this.apiService = apiService;
            this.viewRenderService = viewRenderService;
            this.userManager = userManager;
            this.emailService = emailService;
            this.linkGenerator = linkGenerator;
            this.webHostEnvironment = webHostEnvironment;
        }
        private async Task<string> GetAll()
        {
            return await apiService.GetAsync("Tasks/Emails/ConfirmEmail");
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
                var data = JsonSerializer.Deserialize<ConfirmEmailTaskModel>(task.Data);
                var user = await userManager.FindByIdAsync(data.UserId);
                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

                var callbackUrl = linkGenerator.GetPathByAction("ConfirmEmail", "Account", new { UserId = user.Id, Token = token });
                var appUrl = webHostEnvironment.IsDevelopment() ? "https://localhost:5001" : "https://cinenet.bsite.net";
                var absoluteUrl = appUrl + callbackUrl;
                var model = new ConfirmEmailModel
                {
                    UserName = user.UserName,
                    Url = absoluteUrl
                };
                var view = await viewRenderService.RenderToString("ConfirmEmail", model);
                var emailModel = new EmailInfo("Confirmación de cuenta", view, new List<string> { user.Email })
                {
                    IsBodyHtml = true,
                };
                await emailService.SendEmail(emailModel);
                await this.Delete(task.Id);
            }
        }
    }
}
