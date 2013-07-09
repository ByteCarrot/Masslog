using ByteCarrot.Masslog.Core.Infrastructure.Extensions;
using System.Configuration;
using ByteCarrot.Masslog.Core.DomainModel.Entities;
using ByteCarrot.Masslog.Core.DomainModel.Repositories;

namespace ByteCarrot.Masslog.Core.Logging
{
    public static class ApplicationRepositoryExtensions
    {
        public static Application FindByIdOrThrowException(this IApplicationRepository repository, string applicationId)
        {
            var application = repository.Get(applicationId);
            if (application == null)
            {
                throw new ConfigurationErrorsException("Application ID ({0}) specified in configuration is invalid.".Args(applicationId));
            }
            return application;
        }
    }
}