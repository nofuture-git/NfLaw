using System;
using NoFuture.Law;
using NoFuture.Law.Procedure.Civil.US.Jurisdiction;
using NoFuture.Law.US.Courts;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Procedure.Civil.Tests
{
    public class ExampleTestMinContact
    {
        private readonly ITestOutputHelper output;

        public ExampleTestMinContact(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestMinimumContactIsValid00()
        {
            var testSubject = new MinimumContact(new StateCourt("NV"))
            {
                GetActiveVirtualContactLocation = lp => lp is ExampleDefendant ? new [] {new VocaBase("NV")} : null,
                GetInjuryLocation = lp => new VocaBase("NV")
            };

            var testResult = testSubject.IsValid(new ExamplePlaintiff(), new ExampleDefendant());
            Assert.True(testResult);

            this.output.WriteLine(testSubject.ToString());
        }

        [Fact]
        public void TestMinimumContactIsValid01()
        {

            var testSubject = new MinimumContact(new StateCourt("NV"))
            {
                GetIntentionalTortTo = lp => lp is ExampleDefendant ? new ExamplePlaintiff() : null,
                GetDomicileLocation = lp => lp is ExamplePlaintiff ? new VocaBase("NV") : null,
            };
            var testResult = testSubject.IsValid(new ExamplePlaintiff(), new ExampleDefendant());
            Assert.True(testResult);

            this.output.WriteLine(testSubject.ToString());

        }
    }
}
