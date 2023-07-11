namespace ProyectoFinal.Services
{
    public class EmailOptions
    {
        public bool EnableSsl { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
    }
}
