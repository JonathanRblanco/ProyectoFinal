using ProyectoFinal.Data.Email;

namespace ProyectoFinal.ServicesContracts
{
    public interface IEmailService
    {
        Task SendEmail(EmailInfo emailInfo);
    }
}
