
namespace ByteCarrot.Masslog.Core.Infrastructure.Configuration
{
    public class MasslogConfiguration : ConfigurationBase, IMasslogConfiguration
    {
        public MasslogConfiguration() : base("masslog")
        {
            ConnectionString = GetStringSetting("ConnectionString");
            DatabaseName = GetStringSetting("DatabaseName");
        }

        public string ConnectionString { get; private set; }

        public string DatabaseName { get; private set; }
    }
}