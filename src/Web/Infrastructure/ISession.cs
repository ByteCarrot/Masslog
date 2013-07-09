
namespace ByteCarrot.Masslog.Web.Infrastructure
{
    public interface ISession
    {
        T Get<T>() where T : class;

        void Set<T>(T value) where T : class;

        void Destroy();
    }
}