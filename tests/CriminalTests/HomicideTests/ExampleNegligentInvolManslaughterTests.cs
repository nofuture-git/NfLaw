using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.Homicide;
using NoFuture.Law.Criminal.US.Elements.Intent.PenalCode;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.HomicideTests
{
    public class ExampleNegligentInvolManslaughterTests
    {
        private readonly ITestOutputHelper output;

        public ExampleNegligentInvolManslaughterTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ExampleNegligentInvoluntaryManslaughter()
        {
            var testCrime = new Felony
            {
                ActusReus = new ManslaughterInvoluntary
                {
                    IsCorpusDelicti = lp => lp is StevenSheriffEg
                },
                MensRea = new Negligently
                {
                    IsUnawareOfRisk = lp => lp is StevenSheriffEg,
                    IsUnjustifiableRisk = lp => lp is StevenSheriffEg
                }
            };

            var testResult = testCrime.IsValid(new StevenSheriffEg());
            this.output.WriteLine(testCrime.ToString());
            Assert.True(testResult);
        }
    }

    public class StevenSheriffEg : LegalPerson, IDefendant
    {
        public StevenSheriffEg() : base("STEVEN SHERIFF") { }
    }
}
