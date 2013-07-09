using ByteCarrot.Masslog.Core.DomainModel.Entities;
using ByteCarrot.Masslog.Core.DomainModel.Services.Mailing;

namespace ByteCarrot.Masslog.Core.Logging.Notifications
{
    public class EmailNotification : IEmailNotification
    {
        private readonly IMailingService _mailing;
        private readonly IEmailNotificationRequiredSpecification _specification;

        public EmailNotification(IMailingService mailing, IEmailNotificationRequiredSpecification specification)
        {
            _mailing = mailing;
            _specification = specification;
        }

        public void SendIfRequired(Activity activity)
        {
            if (!_specification.IsSatisfiedBy(activity))
            {
                return;
            }
        }
    }
}