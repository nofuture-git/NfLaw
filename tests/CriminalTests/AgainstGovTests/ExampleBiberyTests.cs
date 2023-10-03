using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.AgainstGov;
using NoFuture.Law.Criminal.US.Elements.Intent.ComLaw;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.AgainstGovTests
{
    public class ExampleBiberyTests
    {
        private readonly ITestOutputHelper output;

        public ExampleBiberyTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestBibery()
        {
            var testCrime = new Felony
            {
                ActusReus = new Bribery
                {
                    IsKnowinglyProcured = lp => lp is IsabelBriberEg,
                    IsPublicOfficial = lp => lp is IsabelBriberEg
                },
                MensRea = new SpecificIntent
                {
                    IsIntentOnWrongdoing = lp => true
                }
            };

            var testResult = testCrime.IsValid(new IsabelBriberEg());
            this.output.WriteLine(testCrime.ToString());
            Assert.True(testResult);
        }
    }

    public class IsabelBriberEg : LegalPerson, IDefendant
    {
        public IsabelBriberEg(): base("ISABEL BRIBER") { }
    }
}
