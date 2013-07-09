using ByteCarrot.Masslog.Core.DataAccess;
using StructureMap.Configuration.DSL;

namespace ByteCarrot.Masslog.Core
{
    public class IocRegistry : Registry
    {
        public IocRegistry()
        {
            For<IMongoDatabaseManager>().Singleton().Use<MongoDatabaseManager>();
        }
    }
}
