using ByteCarrot.Masslog.Core.DomainModel.Entities;

namespace ByteCarrot.Masslog.Core.Logging.Notifications
{
    public interface IEmailNotification
    {
        void SendIfRequired(Activity activity);
    }
}