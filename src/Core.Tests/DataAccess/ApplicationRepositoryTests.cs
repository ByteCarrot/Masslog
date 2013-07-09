using NUnit.Framework;
using ByteCarrot.Masslog.Core.DomainModel.Entities;

namespace ByteCarrot.Masslog.Core.Tests.DataAccess
{
    public class ApplicationRepositoryTests : DomainContextTests
    {
        [Test]
        public void AllowsToSaveNewApplication()
        {
            var app = new Application
                          {
                              Name = "My Sample App", 
                              LoggingRules = "SOME RULE"
                          };
            DomainContext.Applications.Save(app);

            var app2 = DomainContext.Applications.Get(app.Id.ToString());
            Assert.That(app.Id, Is.EqualTo(app2.Id));
            Assert.That(app.Name, Is.EqualTo(app2.Name));
        }
    }
}