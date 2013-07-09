using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ByteCarrot.Masslog.Web.Infrastructure;

namespace ByteCarrot.Masslog.Web.Controllers.Account
{
    public class EditViewModel : ViewModel, IPrivilegesList
    {
        public EditViewModel()
        {
            Privileges = new List<PrivilegesViewModel>();
        }

        [Required]
        public string Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [StringLength(50, MinimumLength = 4)]
        public string Password { get; set; }

        [Compare("Password"), Display(Name = Constants.RepeatPassword)]
        public string Password2 { get; set; }

        [Display(Name = Constants.IsAdministrator)]
        public bool IsAdministrator { get; set; }

        public List<PrivilegesViewModel> Privileges { get; set; }
    }
}