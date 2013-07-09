using ByteCarrot.Masslog.Core.DomainModel.Entities;
using ByteCarrot.Masslog.Core.DomainModel.Repositories;
using ByteCarrot.Masslog.Core.Logging.Notifications;

namespace ByteCarrot.Masslog.Core.Logging
{
    public class ActivityProcessor : IActivityProcessor
    {
        private readonly IDomainContext _context;
        private readonly IEmailNotification _notification;

        public ActivityProcessor(IDomainContext context, IEmailNotification notification)
        {
            _context = context;
            _notification = notification;
        }

        public void Process(Activity activity)
        {
            _context.Activities.Save(activity);
            _notification.SendIfRequired(activity);
        }
    }
}