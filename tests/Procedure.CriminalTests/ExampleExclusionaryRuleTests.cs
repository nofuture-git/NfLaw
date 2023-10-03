using System;
using NoFuture.Law;
using NoFuture.Law.Procedure.Criminal.US.Challenges;
using Xunit;

namespace NoFuture.Law.Procedure.Criminal.Tests
{
    
    public class ExampleExclusionaryRuleTests
    {
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
            Console.WriteLine(testSubject.ToString());
            Assert.IsTrue(testResult);
        }
    }
}
