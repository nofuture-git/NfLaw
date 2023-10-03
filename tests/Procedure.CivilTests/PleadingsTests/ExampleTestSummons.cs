using System;
using NoFuture.Law.Procedure.Civil.US.Pleadings;
using NoFuture.Law.US.Courts;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Procedure.Civil.Tests
{
    public class ExampleTestSummons
    {
        private readonly ITestOutputHelper output;

        public ExampleTestSummons(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestSummonsIsValid()
        {
            var testSubject = new Summons
            {
                Court = new FederalCourt("district"),
                IsSigned = lp => true,
                GetAssertion = lp => new ExampleCauseForAction(),
                GetDateOfAppearance = lp => DateTime.Today.AddDays(30),
            };

            var testResult = testSubject.IsValid(new ExamplePlaintiff(), new ExampleDefendant());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);

        }
    }
}
