using System;
using NUnit.Framework;
using RazorEngine;
using ByteCarrot.Masslog.TestFramework;

namespace ByteCarrot.Masslog.Core.Tests.Logging
{
    public class EmailTests : TestFixtureBase
    {
        [Test]
        public void AAA()
        {
            var result = Razor.Parse(@"
Hello @Model.Text
", new { Text = "Tonny"});
            Console.WriteLine(result);
            
        }
    }
}
