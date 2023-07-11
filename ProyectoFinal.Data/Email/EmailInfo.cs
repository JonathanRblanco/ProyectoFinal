namespace ProyectoFinal.Data.Email
{
    public class EmailInfo
    {
        public EmailInfo(string Subject, string Body, List<string> To)
        {
            this.Subject = Subject;
            this.Body = Body;
            if (To is null)
            {
                throw new ArgumentNullException("La propiedad To no puede ser null");
            }
            if (To.Count == 0)
            {
                throw new ArgumentException("La propiedad To debe contener al menos un elemento");
            }
            this.To = To;
            this.CC = new List<string>();
            this.BCC = new List<string>();
            this.Files = new List<FileModel>();
        }

        public bool Important { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Mailbox { get; set; }
        public bool IsBodyHtml { get; set; }
        public List<string> To { get; set; }
        public List<string> CC { get; set; }
        public List<string> BCC { get; set; }
        public List<FileModel> Files { get; set; }
    }
}
