using System.Web;

namespace ByteCarrot.Masslog.Web.Infrastructure
{
    public class Session : ISession
    {
        public T Get<T>() where T : class
        {
            return HttpContext.Current.Session[typeof (T).FullName] as T;
        }

        public void Set<T>(T value) where T : class
        {
            HttpContext.Current.Session[typeof(T).FullName] = value;
        }

        public void Destroy()
        {
            var session = HttpContext.Current.Session;
            session.Clear();
            session.Abandon();
        }
    }
}