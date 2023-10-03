using System;
using NoFuture.Law.Procedure.Civil.US.Judgment;
using NoFuture.Law.US;
using NoFuture.Law.US.Courts;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Procedure.Civil.Tests.JudgmentTests
{
    public class ExampleTestJnov
    {
        private readonly ITestOutputHelper output;

        public ExampleTestJnov(ITestOutputHelper output)
        {
            this.output = output;
        }


        [Fact]
        public void TestJnovIsValid()
        {
            var testSubject = new JudgmentNotwithstandingVerdict
            {
                Court = new StateCourt("LA"),
                GetSubjectPerson = lps => lps.Plaintiff(),
                GetAssertion = lp => new ExampleCauseForAction(),
                IsCaseWeakBeyondReason = lp => lp is ExampleCauseForAction,
                IsMadeMotionPriorToVerdict = lp => false
            };

            var testResult = testSubject.IsValid(new ExamplePlaintiff(), new ExampleDefendant());
            this.output.WriteLine(testSubject.ToString());
            Assert.False(testResult);

            testSubject.IsMadeMotionPriorToVerdict = lp => lp is ExamplePlaintiff;
            testResult = testSubject.IsValid(new ExamplePlaintiff(), new ExampleDefendant());
            Assert.True(testResult);

        }
    }
}
