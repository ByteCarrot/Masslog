using System.Linq;
using System.Security.Authentication;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Security;
using ByteCarrot.Masslog.Core.DomainModel.Entities;
using ByteCarrot.Masslog.Core.DomainModel.Repositories;

namespace ByteCarrot.Masslog.Web.Infrastructure.Security
{
    public class SecurityService : ISecurityService
    {
        private readonly IDomainContext _context;
        private readonly ISession _session;

        public SecurityService(IDomainContext context, ISession session)
        {
            _context = context;
            _session = session;
        }

        public void AuthenticateRequest(HttpContext httpContext)
        {
            if (!FormsAuthentication.IsEnabled)
            {
                return;
            }

            var cookie = httpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie == null)
            {
                return;
            }

            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            var role = ticket.UserData;
            var principal = new GenericPrincipal(new GenericIdentity(ticket.Name), new[] { role });
            httpContext.User = Thread.CurrentPrincipal = principal;
        }

        public User GetCurrentUser(bool forceRefresh = false)
        {
            var username = HttpContext.Current.User.Identity.Name;
            var data = _session.Get<SecurityData>();
            if (!forceRefresh && data != null && data.CurrentUser.Username == username)
            {
                return data.CurrentUser;
            }

            var user = _context.Users.FindByUsername(username);
            if (user == null)
            {
                throw new AuthenticationException();
            }

            if (user.IsAdministrator)
            {
                user.Privileges = _context.Applications.All()
                        .Select(x => new Privileges {ApplicationId = x.Id, CanModify = true})
                        .ToList();
            }

            _session.Set(new SecurityData(user));
            return user;
        }
    }
}