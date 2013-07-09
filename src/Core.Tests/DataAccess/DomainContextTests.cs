using ByteCarrot.Masslog.Core.DataAccess;
using ByteCarrot.Masslog.Core.Infrastructure.Configuration;
using ByteCarrot.Masslog.TestFramework;

namespace ByteCarrot.Masslog.Core.Tests.DataAccess
{
    public abstract class DomainContextTests : TestFixtureBase
    {
        protected DomainContext DomainContext { get; private set; }

        protected override void SetUp()
        {
            DomainContext = new DomainContext(new MongoDatabaseManager(new MasslogConfiguration()));
        }

        protected override void TearDown()
        {
            DomainContext.Reset();
        }
    }
}
