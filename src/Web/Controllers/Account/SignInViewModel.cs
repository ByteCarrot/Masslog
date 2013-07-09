using System.ComponentModel.DataAnnotations;
using ByteCarrot.Masslog.Web.Infrastructure;

namespace ByteCarrot.Masslog.Web.Controllers.Account
{
    public class SignInViewModel : ViewModel
    {
        [Required, StringLength(50)]
        public string Username { get; set; }

        [Required, StringLength(50)]
        public string Password { get; set; }

        [Display(Name = Constants.RememberMe)]
        public bool RememberMe { get; set; }

        public string Message { get; set; }
    }
}