using NUnit.Framework;
using System;
using ByteCarrot.Masslog.Core.DomainModel.Entities;
using ByteCarrot.Masslog.TestFramework;

namespace ByteCarrot.Masslog.Core.Tests.DomainModel.Entities
{
    public class DenormalizedDateTimeTests : TestFixtureBase
    {
        [Test]
        public void ShouldSetAllPropertiesUsingDataFromPassedDateTime()
        {
            var date = new DateTime(2003, 12, 3, 3, 2, 55);
            var ddt = new DenormalizedDateTime(date);
            Assert.That(ddt.Date, Is.EqualTo(date));
            Assert.That(ddt.Year, Is.EqualTo(2003));
            Assert.That(ddt.Month, Is.EqualTo(12));
            Assert.That(ddt.Day, Is.EqualTo(3));
            Assert.That(ddt.Hour, Is.EqualTo(3));
            Assert.That(ddt.Minute, Is.EqualTo(2));
            Assert.That(ddt.Second, Is.EqualTo(55));
        }
    }
}
