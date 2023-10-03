using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.Act;
using NoFuture.Law.Criminal.US.Elements.Intent.ComLaw;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.MensReaTests
{
    /// <summary>
    /// HAROLD E. STAPLES, III, PETITIONER v. UNITED STATES No. 92-1441
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, not requiring mens rea is mostly for regulations with fines, not felonies
    /// ]]>
    /// </remarks>
    public class HaroldStaplesvUsTests
    {
        private readonly ITestOutputHelper output;

        public HaroldStaplesvUsTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void HaroldStaplesvUs()
        {
            var testSubject = new Felony
            {
                ActusReus = new ActusReus
                {
                    IsAction = lp => lp is HaroldStaples,
                    IsVoluntary = lp => lp is HaroldStaples
                },
                MensRea = null
            };

            //mens rea is not needed for Fed Statute intended to regulate
            //for example ownership of hand gernades was a crime without mens rea
            var testResult = testSubject.IsValid(new HaroldStaples());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);

            //for this case, gun ownership is not the same thing
            testSubject.MensRea = new GeneralIntent();
            testResult = testSubject.IsValid(new HaroldStaples());
            this.output.WriteLine(testSubject.ToString());
            Assert.False(testResult);
        }
    }

    public class HaroldStaples : LegalPerson, IDefendant
    {
        public HaroldStaples(): base("HAROLD E. STAPLES") { }
        public bool IsPossessionOfAr15Rifle => true;
        public bool IsAr15RifleFullyAutoFire => true;
    }
}
