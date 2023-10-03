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
    /// State v. Guido, 191 A.2d 45 (1993)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, the mental defect has meaning in its effect, not as a medical label
    /// ]]>
    /// </remarks>
    public class StatevGuidoTests
    {
        private readonly ITestOutputHelper output;

        public StatevGuidoTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void StatevGuido()
        {
            var testCrime = new Felony
            {
                ActusReus = new ActusReus
                {
                    IsVoluntary = lp => lp is Guido,
                    IsAction = lp => lp is Guido,
                },
                MensRea = new MaliceAforethought
                {
                    IsKnowledgeOfWrongdoing = lp => lp is Guido,
                }
            };

            var testResult = testCrime.IsValid(new Guido());
            Assert.True(testResult);

            var testSubject = new MNaghten
            {
                IsMentalDefect = lp => lp is Guido,
                IsWrongnessOfAware = lp => !(lp is Guido),
            };

            testResult = testSubject.IsValid(new Guido());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);

        }
    }

    public class Guido : LegalPerson, IDefendant
    {
        public Guido(): base("ADELE GUIDO") {}
    }
}
