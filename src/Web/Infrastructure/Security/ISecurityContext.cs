using System.Collections.Generic;

namespace ByteCarrot.Masslog.Web.Infrastructure.Security
{
    public interface ISecurityContext
    {
        string Username { get; }

        bool IsAuthenticated { get; }

        bool CanAccessApplications { get; }

        bool CanAccessAdministration { get; }

        bool CanManageUsers { get; }

        bool CanManageApplications { get; }

        bool CanManageSmtpConfiguration { get; }

        bool CanChangePassword { get; }

        bool CanModifyApplication(string applicationId);

        bool CanAccessApplication(string applicationId);

        IList<string> ReadAccess { get; }

        void Refresh();
    }
}