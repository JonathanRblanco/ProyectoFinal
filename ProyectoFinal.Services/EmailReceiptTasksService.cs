using Microsoft.AspNetCore.Identity;
using ProyectoFinal.Data;
using ProyectoFinal.Data.Email;
using ProyectoFinal.DTO.Responses;
using ProyectoFinal.ServicesContracts;
using System.Text.Json;

namespace ProyectoFinal.Services
{
    public class EmailReceiptTasksService : ITasksService
    {
        private readonly IApiService apiService;
        private readonly IViewRenderService viewRenderService;
        private readonly UserManager<User> userManager;
        private readonly IEmailService emailService;
        private readonly ICheckOutService checkOutService;
        private readonly IPdfService pdfService;

        public EmailReceiptTasksService(IApiService apiService,
            IViewRenderService viewRenderService,
            UserManager<User> userManager,
            IEmailService emailService,
            ICheckOutService checkOutService,
            IPdfService pdfService)
        {
            this.apiService = apiService;
            this.viewRenderService = viewRenderService;
            this.userManager = userManager;
            this.emailService = emailService;
            this.checkOutService = checkOutService;
            this.pdfService = pdfService;
        }
        private async Task<string> GetAll()
        {
            return await apiService.GetAsync("Tasks/Emails/Receipts");
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
                try
                {
                    var data = JsonSerializer.Deserialize<ReceiptTaskModel>(task.Data);
                    var user = await userManager.FindByIdAsync(data.UserId);
                    var receipt = await checkOutService.GetById<GetPaymentsResponse>(data.ReceiptId);
                    var receiptModel = new ReceiptEmailModel
                    {
                        UserName = user.UserName,
                        AmountOfTickets = receipt.AmountOfTickets,
                        Code = receipt.Code,
                        Date = receipt.Show.Date,
                        Room = receipt.Show.Room.Number,
                        Total = receipt.Total,
                        MovieName = receipt.Show.Movie.Name + " - " + receipt.Show.ShowType.Description,
                        BranchCinemaName = receipt.Show.Room.CinemaBranch.Name,
                        Address = string.Concat(receipt.Show.Room.CinemaBranch.Address, ", ", receipt.Show.Room.CinemaBranch.Location, ", ", receipt.Show.Room.CinemaBranch.CP),
                        BranchPhone = receipt.Show.Room.CinemaBranch.Phone,
                        BranchEmail = receipt.Show.Room.CinemaBranch.Email
                    };
                    var receiptPdf = pdfService.MakeReceiptPdf(new PdfReceipt
                    {
                        AmountOfTickets = receipt.AmountOfTickets.ToString(),
                        Code = receipt.Code,
                        Date = receipt.Show.Date.ToString("dd/MM/yyyy"),
                        Movie = receipt.Show.Movie.Name,
                        Room = receipt.Show.Room.Number.ToString(),
                        ShowType = receipt.Show.ShowType.Description,
                        Time = receipt.Show.Date.ToString("HH:mm"),
                        Total = receipt.Total.ToString(),
                        Address = string.Concat(receipt.Show.Room.CinemaBranch.Address, ", ", receipt.Show.Room.CinemaBranch.Location, ", ", receipt.Show.Room.CinemaBranch.CP),
                        BranchCinemaName = receipt.Show.Room.CinemaBranch.Name
                    });
                    var view = await viewRenderService.RenderToString("ReceiptEmail", receiptModel);
                    var emailInfo = new EmailInfo("Confirmación de su Operación", view, new List<string> { user.Email });
                    emailInfo.IsBodyHtml = true;
                    emailInfo.Files.Add(new FileModel
                    {
                        Data = receiptPdf,
                        FileType = "application/pdf",
                        Name = "Recibo de compra"
                    });
                    await emailService.SendEmail(emailInfo);
                    await this.Delete(task.Id);
                }
                catch
                {

                }
            }
        }
    }
}
