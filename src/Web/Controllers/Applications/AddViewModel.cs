using System.ComponentModel.DataAnnotations;
using ByteCarrot.Masslog.Core.DomainModel.Entities;
using ByteCarrot.Masslog.Core.Rules;
using ByteCarrot.Masslog.Web.Infrastructure;

namespace ByteCarrot.Masslog.Web.Controllers.Applications
{
    public class AddViewModel : ViewModel
    {
        [Required, StringLength(250)]
        public string Name { get; set; }

        public Application ToApplication()
        {
            return new Application
                       {
                           Name = Name,
                           LoggingRules = Rule.Default
                       };
        }
    }
}