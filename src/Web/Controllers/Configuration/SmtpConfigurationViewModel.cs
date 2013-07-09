using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ByteCarrot.Masslog.Web.Infrastructure;

namespace ByteCarrot.Masslog.Web.Controllers.Configuration
{
    public class SmtpConfigurationViewModel : ViewModel
    {
        [Required]
        public string Host { get; set; }

        [Required]
        public int Port { get; set; }

        [DisplayName(Constants.Domain)]
        public string Domain { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        [DisplayName(Constants.EnableSsl)]
        public bool EnableSsl { get; set; }
    }
}