namespace ByteCarrot.Masslog.Web.Infrastructure.Security
{
    public class CanManageUsersAttribute : CanAttribute
    {
        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            return Security.CanManageUsers;
        }
    }
}