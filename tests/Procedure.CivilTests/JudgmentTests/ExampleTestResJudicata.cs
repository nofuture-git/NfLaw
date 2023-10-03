using System;
using NoFuture.Law.Procedure.Civil.US.Judgment;
using NoFuture.Law.US.Courts;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Procedure.Civil.Tests.JudgmentTests
{
    public class ExampleTestResJudicata
    {
        private readonly ITestOutputHelper output;

        public ExampleTestResJudicata(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestResJudicataIsValid()
        {
            var testSubject = new ResJudicata
            {
                GetAssertion = lp => new ExampleCauseForAction(),
                Court = new StateCourt("KS"),
                IsFinalJudgment = lc => true,
                IsJudgmentBasedOnMerits = lc => true,
                IsSameQuestionOfLawOrFact = lc => lc is ExampleCauseForAction,
                IsSamePartyAsPrior = lp => lp is ExamplePlaintiff || lp is ExampleDefendant
            };

            var testResult = testSubject.IsValid(new ExamplePlaintiff(), new ExampleDefendant());
            Assert.True(testResult);
            this.output.WriteLine(testSubject.ToString());
        }
    }
}
