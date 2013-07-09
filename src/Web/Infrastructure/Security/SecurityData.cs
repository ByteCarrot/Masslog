using ByteCarrot.Masslog.Core.DomainModel.Entities;

namespace ByteCarrot.Masslog.Web.Infrastructure.Security
{
    public class SecurityData
    {
        public SecurityData(User user)
        {
            CurrentUser = user;
        }

        public User CurrentUser { get; private set; }
    }
}