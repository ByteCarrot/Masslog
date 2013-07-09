namespace ByteCarrot.Masslog.Web.Infrastructure.Security
{
    public class CanChangePasswordAttribute : CanAttribute
    {
        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            return Security.CanChangePassword;
        }
    }
}