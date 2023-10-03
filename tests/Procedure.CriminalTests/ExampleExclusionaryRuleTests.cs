using System;
using NoFuture.Law;
using NoFuture.Law.Procedure.Criminal.US.Challenges;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Procedure.Criminal.Tests
{
    public class ExampleExclusionaryRuleTests
    {
        private readonly ITestOutputHelper output;

        public ExampleExclusionaryRuleTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestExclusionaryRuleIsValid00()
        {
            var testSubject = new ExclusionaryRule<IVoca>
            {
                GetEvidence = lp => lp is ExampleSuspect ? new ExampleContraband() : null,
                IsObtainedThroughUnlawfulMeans = l => l is ExampleContraband,
                IsDirectlyEffectedByViolation = lp => lp is ExampleSuspect,
                IsUseInCriminalTrial = l => l is ExampleContraband
            };

            var testResult = testSubject.IsValid(new ExampleSuspect(), new ExampleLawEnforcement());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
        }
    }
}
