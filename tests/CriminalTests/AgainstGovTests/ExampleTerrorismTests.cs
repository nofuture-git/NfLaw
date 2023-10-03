using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.AgainstGov;
using NoFuture.Law.Criminal.US.Elements.Intent.ComLaw;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.AgainstGovTests
{
    /// <summary>
    /// (U.S. v. Moussaoui, 2011)
    /// </summary>
    public class ExampleTerrorismTests
    {
        private readonly ITestOutputHelper output;

        public ExampleTerrorismTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TerrorismTest()
        {
            var testCrime = new Felony
            {
                ActusReus = new Terrorism
                {
                    IsByViolence = lp => lp is ZacariasMoussaoui,
                    IsSocioPoliticalObjective = lp => lp is ZacariasMoussaoui
                },
                MensRea = new MaliceAforethought
                {
                    IsIntentOnWrongdoing = lp => lp is ZacariasMoussaoui,
                    IsKnowledgeOfWrongdoing = lp => lp is ZacariasMoussaoui
                }
            };
            var testResult = testCrime.IsValid(new ZacariasMoussaoui());
            this.output.WriteLine(testCrime.ToString());
            Assert.True(testResult);
        }
    }

    public class ZacariasMoussaoui : LegalPerson, IDefendant
    {
        public ZacariasMoussaoui(): base("ZACARIAS MOUSSAOUI") { }
    }
}
