using System.Collections.Generic;
using System.Linq;
using System.Web;
using ByteCarrot.Masslog.Core.DomainModel.Entities;

namespace ByteCarrot.Masslog.Web.Infrastructure.Security
{
    public class SecurityContext : ISecurityContext
    {
        private readonly ISecurityService _service;
        private User _currentUser;

        public SecurityContext(ISecurityService service)
        {
            _service = service;
            if (!IsAuthenticated)
            {
                return;
            }
            _currentUser = _service.GetCurrentUser();
        }

        public bool IsAuthenticated
        {
            get
            {
                return HttpContext.Current != null && HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated;
            }
        }

        public string Username
        {
            get { return IsAuthenticated ? _currentUser.Username : null; }
        }

        public bool CanAccessApplications
        {
            get { return IsAuthenticated && _currentUser.Privileges.Count > 0; }
        }

        public bool CanManageUsers
        {
            get { return IsAuthenticated && _currentUser.IsAdministrator; }
        }

        public bool CanManageApplications
        {
            get { return IsAuthenticated && _currentUser.IsAdministrator; }
        }

        public bool CanManageSmtpConfiguration 
        {
            get { return IsAuthenticated && _currentUser.IsAdministrator; }
        }

        public bool CanAccessAdministration
        {
            get { return IsAuthenticated && (CanManageUsers || CanManageApplications); }
        }

        public bool CanChangePassword
        {
            get { return IsAuthenticated; }
        }

        public bool CanModifyApplication(string applicationId)
        {
            return IsAuthenticated && _currentUser.Privileges.Any(x => x.ApplicationId == applicationId && x.CanModify);
        }

        public bool CanAccessApplication(string applicationId)
        {
            return IsAuthenticated && _currentUser.Privileges.Any(x => x.ApplicationId == applicationId);
        }

        public IList<string> ReadAccess
        {
            get { return IsAuthenticated ? _currentUser.Privileges.Select(x => x.ApplicationId).ToList() : new List<string>(); }
        }

        public void Refresh()
        {
            if (!IsAuthenticated)
            {
                _currentUser = null;
                return;
            }
            _currentUser = _service.GetCurrentUser(true);
        }
    }
}