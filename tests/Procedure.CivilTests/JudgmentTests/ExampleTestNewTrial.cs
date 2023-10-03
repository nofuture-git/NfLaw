using System;
using NoFuture.Law.Procedure.Civil.US.Judgment;
using NoFuture.Law.US;
using NoFuture.Law.US.Courts;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Procedure.Civil.Tests.JudgmentTests
{
    public class ExampleTestNewTrial
    {
        private readonly ITestOutputHelper output;

        public ExampleTestNewTrial(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestNewTrialIsValid00()
        {
            var testSubject = new NewTrial
            {
                Court = new StateCourt("TN"),
                GetSubjectPerson = lps => lps.Plaintiff(),
                GetAssertion = lp => new ExampleCauseForAction(),
                IsCommittedProceduralError = lp => lp is ExampleCourtJudge
            };
            var testResult =
                testSubject.IsValid(new ExamplePlaintiff(), new ExampleDefendant(), new ExampleCourtJudge());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);

        }

        [Fact]
        public void TestNewTrialIsValid01()
        {
            var testSubject = new NewTrial
            {
                Court = new StateCourt("TN"),
                GetSubjectPerson = lps => lps.Plaintiff(),
                GetAssertion = lp => new ExampleCauseForAction(),
                IsJurySeriouslyErroneousResult = lc => lc is ExampleCauseForAction
            };
            var testResult =
                testSubject.IsValid(new ExamplePlaintiff(), new ExampleDefendant(), new ExampleCourtJudge());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);

        }

        [Fact]
        public void TestNewTrialIsValid02()
        {
            var testSubject = new NewTrial
            {
                Court = new StateCourt("TN"),
                GetSubjectPerson = lps => lps.Plaintiff(),
                GetAssertion = lp => new ExampleCauseForAction(),
                IsNeededToPreventInjustice = lc => lc is ExampleCauseForAction
            };
            var testResult =
                testSubject.IsValid(new ExamplePlaintiff(), new ExampleDefendant(), new ExampleCourtJudge());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);

        }
    }

    public class ExampleCourtJudge : LegalPerson, ICourtOfficial
    {
        public ExampleCourtJudge(): base("Judge M.B. Judjinbad") { }
    }
}
