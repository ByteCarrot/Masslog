using ByteCarrot.Masslog.Core.DomainModel.Entities;

namespace ByteCarrot.Masslog.Core.Logging.Notifications
{
    public interface IEmailNotificationRequiredSpecification
    {
        bool IsSatisfiedBy(Activity activity);
    }
}