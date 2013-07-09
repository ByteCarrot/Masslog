namespace ByteCarrot.Masslog.Core.DomainModel.Repositories
{
    public interface IDomainContext
    {
        IActivityRepository Activities { get; }

        IUserRepository Users { get; }

        IApplicationRepository Applications { get; }

        IConfigurationRepository Configuration { get; }
    }
}