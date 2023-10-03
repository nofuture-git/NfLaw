using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Defense.Excuse;
using NoFuture.Law.Criminal.US.Elements.Act;
using NoFuture.Law.Criminal.US.Elements.Intent.ComLaw;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.DefenseTests
{
    public class ExampleMistakeOfLawTests
    {
        private readonly ITestOutputHelper output;

        public ExampleMistakeOfLawTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ExampleMistakeOfLaw()
        {
            var testCrime = new Felony
            {
                ActusReus = new ActusReus
                {
                    IsAction = lp => lp is ShelbyEg,
                    IsVoluntary = lp => lp is ShelbyEg,
                },
                MensRea = new GeneralIntent
                {
                    IsIntentOnWrongdoing = lp => lp is ShelbyEg,
                }
            };

            var testResult = testCrime.IsValid(new ShelbyEg());
            Assert.True(testResult);

            var testSubject = new MistakeOfLaw
            {
                IsRelianceOnStatementOfLaw = lp => lp is ShelbyEg,
                IsStatementOfLawNowInvalid = lp => lp is ShelbyEg
            };

            testResult = testSubject.IsValid(new ShelbyEg());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
        }
    }

    public class ShelbyEg : LegalPerson, IDefendant
    {
        public ShelbyEg() : base("SHELBY ATTORNEY") { }
    }
}
