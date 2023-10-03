using System;
using System.Linq;
using NoFuture.Law.Procedure.Civil.US;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Procedure.Civil.Tests
{
    public class ExampleTestDomicileLocation
    {
        private readonly ITestOutputHelper output;

        public ExampleTestDomicileLocation(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestDomicileLocationIsValid()
        {
            var testSubject = new DomicileLocation("Ohio")
            {
                IsIntendsIndefiniteStay = lp => lp is ExamplePlaintiff,
                IsPhysicallyPresent = lp => lp is ExamplePlaintiff,
                GetSubjectPerson = lps => lps.FirstOrDefault(l => l is ExamplePlaintiff)
            };

            var testResult = testSubject.IsValid(new ExamplePlaintiff());
            Assert.True(testResult);

            testResult = testSubject.IsValid(new ExampleDefendant());
            Assert.False(testResult);
            this.output.WriteLine(testSubject.ToString());
        }
    }
}
