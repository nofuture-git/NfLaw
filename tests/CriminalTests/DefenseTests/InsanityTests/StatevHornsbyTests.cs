using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Defense.Excuse.Insanity;
using NoFuture.Law.Criminal.US.Elements.Act;
using NoFuture.Law.Criminal.US.Elements.Intent.ComLaw;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.DefenseTests.InsanityTests
{
    /// <summary>
    /// State v. Hornsby, 484 S.E.2d 869 (1997)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, guilty-but-mentally-ill is a half-baked insanity where the only part that is 
    /// true is the mental defect part
    /// ]]>
    /// </remarks>
    public class StatevHornsbyTests
    {
        private readonly ITestOutputHelper output;

        public StatevHornsbyTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void StatevHornsby()
        {
            var testCrime = new Felony
            {
                ActusReus = new ActusReus
                {
                    IsAction = lp => lp is Hornsby,
                    IsVoluntary = lp => lp is Hornsby,
                },
                MensRea = new GeneralIntent
                {
                    IsKnowledgeOfWrongdoing = lp => lp is Hornsby,
                    IsIntentOnWrongdoing = lp => lp is Hornsby,
                }
            };

            var testResult = testCrime.IsValid(new Hornsby());
            Assert.True(testResult);

            var testSubject = new MNaghten
            {
                IsMentalDefect = lp => lp is Hornsby,
            };
            testResult = testSubject.IsValid(new Hornsby());
            this.output.WriteLine(testSubject.ToString());
            Assert.False(testResult);
        }
    }

    public class Hornsby : LegalPerson, IDefendant
    {
        public Hornsby() : base("BRENT HORNSBY") { }
    }
}
