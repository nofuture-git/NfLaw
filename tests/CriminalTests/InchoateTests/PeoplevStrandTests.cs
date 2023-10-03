using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.Inchoate;
using NoFuture.Law.Criminal.US.Elements.Intent.ComLaw;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.InchoateTests
{
    /// <summary>
    /// People v. Strand, 539 N.W.2d 739 (1995)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, one does not commit an assault intending to attempt to commit a crime
    /// ]]>
    /// </remarks>
    public class PeoplevStrandTests
    {
        private readonly ITestOutputHelper output;

        public PeoplevStrandTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void PeoplevStrand()
        {
            var testCrime = new Felony
            {
                MensRea = new GeneralIntent
                {
                    IsIntentOnWrongdoing = lp => lp is Strand
                },
                ActusReus = new Attempt
                {
                }
            };

            var testResult = testCrime.IsValid(new Strand());
            this.output.WriteLine(testCrime.ToString());
            Assert.False(testResult);
        }
    }

    public class Strand : LegalPerson, IDefendant
    {
        
    }
}
