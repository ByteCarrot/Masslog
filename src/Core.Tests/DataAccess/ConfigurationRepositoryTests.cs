using NUnit.Framework;

namespace ByteCarrot.Masslog.Core.Tests.DataAccess
{
    public class ConfigurationRepositoryTests : DomainContextTests
    {
        [Test]
        public void ShouldReturnDefaultSmtpConfigurationIfDoesntExist()
        {
            var c = DomainContext.Configuration.GetSmtpConfiguration();

            Assert.That(c, Is.Not.Null);
            Assert.That(c.Host, Is.EqualTo("localhost"));
        }

        [Test]
        public void ShouldBeAbleToGetSmtpConfiguration()
        {
            var c = DomainContext.Configuration.GetSmtpConfiguration();

            Assert.That(c, Is.Not.Null);
            Assert.That(c.Host, Is.EqualTo("localhost"));
        }

        [Test]
        public void ShouldBeAbleToUpdateSmtpConfiguration()
        {
            var c = DomainContext.Configuration.GetSmtpConfiguration();
            Assert.That(c.Host, Is.EqualTo("localhost"));
            c.Host = "127.0.0.1";
            
            DomainContext.Configuration.Save(c);

            var c2 = DomainContext.Configuration.GetSmtpConfiguration();
            Assert.That(c2.Host, Is.EqualTo("127.0.0.1"));
        }
    }
}