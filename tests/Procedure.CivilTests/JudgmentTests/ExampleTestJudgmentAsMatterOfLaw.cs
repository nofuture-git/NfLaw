using NoFuture.Law.Procedure.Civil.US.Judgment;
using NoFuture.Law.US;
using NoFuture.Law.US.Courts;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Procedure.Civil.Tests.JudgmentTests
{
    public class ExampleTestJudgmentAsMatterOfLaw
    {
        private readonly ITestOutputHelper output;

        public ExampleTestJudgmentAsMatterOfLaw(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestJudgmentAsMatterOfLawIsValid()
        {
            var testSubject = new JudgmentAsMatterOfLaw()
            {
                GetSubjectPerson = lps => lps.Plaintiff(),
                Court = new StateCourt("MS"),
                GetAssertion = lp => new ExampleCauseForAction(),
                IsCaseWeakBeyondReason = lc => lc is ExampleCauseForAction
            };

            var testResult = testSubject.IsValid(new ExamplePlaintiff(), new ExampleDefendant());
            Assert.True(testResult);
        }
    }
}
