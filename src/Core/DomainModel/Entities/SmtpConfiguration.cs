using System.Net;
using System.Net.Mail;

namespace ByteCarrot.Masslog.Core.DomainModel.Entities
{
    public class SmtpConfiguration : ConfigurationEntity
    {
        public string Host { get; set; }

        public int Port { get; set; }

        protected string Domain { get; set; }

        protected string Username { get; set; }

        protected string Password { get; set; }

        protected bool EnableSsl { get; set; }

        public static SmtpConfiguration Default
        {
            get
            {
                return new SmtpConfiguration
                           {
                               Host = "localhost",
                               Port = 25,
                           };
            }
        }

        public SmtpClient CreateSmtpClient()
        {
            return new SmtpClient(Host, Port)
                       {
                           EnableSsl = EnableSsl,
                           Credentials = new NetworkCredential(Username, Password, Domain)
                       };
        }
    }
}