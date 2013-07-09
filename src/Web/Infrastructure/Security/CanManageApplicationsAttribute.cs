namespace ByteCarrot.Masslog.Web.Infrastructure.Security
{
    public class CanManageApplicationsAttribute : CanAttribute
    {
        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            return Security.CanManageApplications;
        }
    }
}