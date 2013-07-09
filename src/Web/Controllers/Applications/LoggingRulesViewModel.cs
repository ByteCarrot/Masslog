using System.ComponentModel.DataAnnotations;
using ByteCarrot.Masslog.Web.Infrastructure;

namespace ByteCarrot.Masslog.Web.Controllers.Applications
{
    public class LoggingRulesViewModel : ViewModel
    {
        [Required]
        public string Rules { get; set; }
    }
}