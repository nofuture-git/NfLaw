using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.Act;
using NoFuture.Law.Criminal.US.Elements.Intent.ComLaw;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.ActusReusTests
{
    /// <summary>
    /// Regalado v. U.S., 572 A.2d 416 (1990)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, criminal intent as general intent with malice
    /// ]]>
    /// </remarks>
    public class RegaladovUsTests
    {
        private readonly ITestOutputHelper output;

        public RegaladovUsTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void RegaladovUs()
        {
            var testSubject = new Felony
            {
                ActusReus = new ActusReus
                {
                    IsVoluntary = lp => lp is Regalado,
                    IsAction = lp => lp is Regalado
                },
                MensRea = new GeneralIntent
                {
                    IsIntentOnWrongdoing = lp => lp is Regalado,
                    IsKnowledgeOfWrongdoing = lp => lp is Regalado
                }
            };

            var testResult = testSubject.IsValid(new Regalado());
            Assert.True(testResult);
        }
    }

    public class Regalado : LegalPerson, IDefendant
    {

    }
}
