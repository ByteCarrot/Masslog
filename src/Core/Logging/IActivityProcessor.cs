using ByteCarrot.Masslog.Core.DomainModel.Entities;

namespace ByteCarrot.Masslog.Core.Logging
{
    public interface IActivityProcessor
    {
        void Process(Activity activity);
    }
}