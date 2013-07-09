using System.ComponentModel.DataAnnotations;
using ByteCarrot.Masslog.Web.Infrastructure;

namespace ByteCarrot.Masslog.Web.Controllers.Account
{
    public class ChangePasswordViewModel : ViewModel
    {
        public string Username { get; set; }

        [Display(Name = Constants.OldPassword), Required, StringLength(50)]
        public string OldPassword { get; set; }

        [Display(Name = Constants.NewPassword),
        Required, StringLength(50, MinimumLength = 4)]
        public string NewPassword { get; set; }

        [Display(Name = Constants.RepeatNewPassword),
        Required, System.Web.Mvc.Compare("NewPassword", ErrorMessage = Constants.PasswordsDoNotMatch)]
        public string NewPassword2 { get; set; }
    }
}