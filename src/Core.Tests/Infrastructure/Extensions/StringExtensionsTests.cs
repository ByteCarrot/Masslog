using ByteCarrot.Masslog.Core.Infrastructure.Extensions;
using NUnit.Framework;
using System;
using ByteCarrot.Masslog.TestFramework;

namespace ByteCarrot.Masslog.Core.Tests.Infrastructure.Extensions
{
    public class StringExtensionsTests : TestFixtureBase
    {
        [Test]
        public void Md5Hash_OutputStringHasToBeDifferentThanInputString()
        {
            Assert.That("some string".Md5Hash(), Is.Not.EqualTo("some string"));
        }

        [Test]
        public void Md5Hash_ShouldAlwaysCreateTheSameHashForSpecifiedSting()
        {
            Assert.That("some string".Md5Hash(), Is.EqualTo("some string".Md5Hash()));
        }

        [Test, ExpectedException(typeof(NullReferenceException))]
        public void Md5Hash_ForNullStringShouldThrowException()
        {
            ((string)null).Md5Hash();
        }
    }
}
