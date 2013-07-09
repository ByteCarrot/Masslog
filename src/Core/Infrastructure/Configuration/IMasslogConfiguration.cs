
namespace ByteCarrot.Masslog.Core.Infrastructure.Configuration
{
    public interface IMasslogConfiguration
    {
        string ConnectionString { get; }

        string DatabaseName { get; }
    }
}