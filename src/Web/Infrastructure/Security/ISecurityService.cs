using System.Web;
using ByteCarrot.Masslog.Core.DomainModel.Entities;

namespace ByteCarrot.Masslog.Web.Infrastructure.Security
{
    public interface ISecurityService
    {
        void AuthenticateRequest(HttpContext httpContext);
        
        User GetCurrentUser(bool forceRefresh = false);
    }
}