using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ByteCarrot.Masslog.Web.Infrastructure;
using ByteCarrot.Masslog.Web.Infrastructure.Validation;

namespace ByteCarrot.Masslog.Web.Controllers.Account
{
    public class AddViewModel : ViewModel, IPrivilegesList
    {
        public AddViewModel()
        {
            Privileges = new List<PrivilegesViewModel>();
        }

        [Required, StringLength(50, MinimumLength = 4), NoWhiteSpacesOrSpecialChars]
        public string Username { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, StringLength(50, MinimumLength = 4)]
        public string Password { get; set; }

        [Required, Compare("Password"), Display(Name = Constants.RepeatPassword)]
        public string Password2 { get; set; }

        [Display(Name = Constants.IsAdministrator)]
        public bool IsAdministrator { get; set; }

        public List<PrivilegesViewModel> Privileges { get; set; } 
    }
}