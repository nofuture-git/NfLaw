using System;
using NoFuture.Law.Procedure.Civil.US.Judgment;
using NoFuture.Law.US.Courts;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Procedure.Civil.Tests.JudgmentTests
{
    public class ExampleTestConsentJudgment
    {
        private readonly ITestOutputHelper output;

        public ExampleTestConsentJudgment(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestConsentJudgmentIsValid()
        {
            var testSubject = new ConsentJudgment
            {
                Court = new StateCourt("California"),
                IsApprovalExpressed = lp => lp is ConsentJudgmentPlaintiff00
                                            || lp is ConsentJudgmentPlaintiff01
                                            || lp is ConsentJudgmentDefendant00
                                            || lp is ConsentJudgmentDefendant01
            };

            var testResult = testSubject.IsValid(new ConsentJudgmentPlaintiff00(), new ConsentJudgmentPlaintiff01(),
                new ConsentJudgmentDefendant00(), new ConsentJudgmentDefendant01());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
        }
    }

    public class ConsentJudgmentPlaintiff00 : LegalPerson, IPlaintiff { }

    public class ConsentJudgmentPlaintiff01 : LegalPerson, IPlaintiff { }

    public class ConsentJudgmentDefendant00 : LegalPerson, IDefendant { }

    public class ConsentJudgmentDefendant01 : LegalPerson, IDefendant { }
}
