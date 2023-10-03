using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.AgainstPersons;
using NoFuture.Law.Criminal.US.Elements.AgainstPersons.Credible;
using NoFuture.Law.Criminal.US.Elements.Intent.PenalCode;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.HominiLupusTests
{
    /// <summary>
    /// State v. Holbach, 2009 ND 37 (2009)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, the agitation must induce a real fear
    /// ]]>
    /// </remarks>
    public class StatevHolbachTests
    {
        private readonly ITestOutputHelper output;

        public StatevHolbachTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void StatevHolbach()
        {
            var testCrime = new Felony
            {
                ActusReus = new Stalking
                {
                    IsApparentAbility = lp => lp is Holbach,
                    Occasions = new IAgitate[]
                    {
                        new ThreateningWords
                        {
                            IsCauseToFearSafety = lp => lp is Holbach,
                        }, 
                        new ThreateningWords
                        {
                            IsCauseToFearSafety = lp => lp is Holbach
                        }
                    }
                },
                MensRea = new Purposely
                {
                    IsKnowledgeOfWrongdoing = lp => lp is Holbach
                }
            };

            var testResult = testCrime.IsValid(new Holbach());
            this.output.WriteLine(testCrime.ToString());
            Assert.True(testResult);
        }
    }

    public class Holbach : LegalPerson, IDefendant
    {
        public Holbach() : base("SCOTT THOMAS BOYLE") { }
    }
}
