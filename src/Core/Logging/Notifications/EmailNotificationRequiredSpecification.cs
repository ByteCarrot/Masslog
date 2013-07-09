using ByteCarrot.Masslog.Core.DomainModel.Entities;

namespace ByteCarrot.Masslog.Core.Logging.Notifications
{
    public class EmailNotificationRequiredSpecification : IEmailNotificationRequiredSpecification
    {
        public bool IsSatisfiedBy(Activity activity)
        {
            return activity.Status == ActivityStatus.Failure &&
                   activity.FailureDeterminedBy == FailureDeterminedBy.Exception;
        }
    }
}