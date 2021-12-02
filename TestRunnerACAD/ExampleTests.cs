using System.Threading;
using NUnit.Framework;

namespace TestRunnerACAD
{
    [TestFixture, Apartment(ApartmentState.STA)]
    public class ExampleTests : TestBase
    {
        [Test]
        public void Test_Pass()
        {
            Assert.Pass("This test should always passes.");
        }
    }
}