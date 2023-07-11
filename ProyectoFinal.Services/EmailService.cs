using Microsoft.Extensions.Options;
using ProyectoFinal.Data.Email;
using ProyectoFinal.ServicesContracts;
using System.Net;
using System.Net.Mail;

namespace ProyectoFinal.Services
{
    public class EmailService : IEmailService
    {
        private readonly IOptions<EmailOptions> options;

        public EmailService(IOptions<EmailOptions> options)
        {
            this.options = options;
        }
        public async Task SendEmail(EmailInfo emailInfo)
        {
            var credentials = options.Value;
            using SmtpClient client = new()
            {
                Host = credentials.Host,
                Port = credentials.Port,
                EnableSsl = credentials.EnableSsl,
                Credentials = new NetworkCredential(credentials.UserEmail, credentials.Password)
            };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var mail = new MailMessage();
            emailInfo.To.ForEach(x =>
            {
                mail.To.Add(x);
            });
            emailInfo.CC.ForEach(x =>
            {
                mail.CC.Add(x);
            });
            emailInfo.BCC.ForEach(x =>
            {
                mail.Bcc.Add(x);
            });
            mail.From = new MailAddress(credentials.UserEmail);
            mail.Subject = emailInfo.Subject;
            mail.Body = emailInfo.Body;
            mail.IsBodyHtml = emailInfo.IsBodyHtml;
            if (emailInfo.Important)
            {
                mail.Priority = MailPriority.High;
            }
            if (emailInfo.Files != null && emailInfo.Files.Count > 0)
            {
                emailInfo.Files.ForEach(x =>
                {
                    MemoryStream ms = new MemoryStream(x.Data);
                    Attachment archivoAdjunto = new Attachment(ms, x.Name, x.FileType);
                    mail.Attachments.Add(archivoAdjunto);
                });
            }
            await client.SendMailAsync(mail);
        }
    }
}
