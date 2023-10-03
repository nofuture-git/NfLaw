using System;
using NoFuture.Law;
using NoFuture.Law.Procedure.Criminal.US.Challenges;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Procedure.Criminal.Tests
{
    public class ExampleDerivativeExclusionaryRuleTests
    {
        private readonly ITestOutputHelper output;

        public ExampleDerivativeExclusionaryRuleTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestDerivativeExclusionaryRuleIsValid00()
        {
            var testSubject = new DerivativeExclusionaryRule<IVoca>
            {
                IsDerivedFromUnlawfulSource = v => true,
                IsInterveningEventsAttenuated = v => v is ExampleContraband,
                GetDerivativeEvidence = lp => lp is ExampleSuspect ? new ExampleContraband() : null,
            };

            var testResult = testSubject.IsValid(new ExampleSuspect(), new ExampleLawEnforcement());
            this.output.WriteLine(testSubject.ToString());
            Assert.False(testResult);

        }
    }
}
