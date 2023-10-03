using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.AgainstGov;
using NoFuture.Law.Criminal.US.Elements.Intent.PenalCode;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.AgainstGovTests
{
    public class ExamplePerjuryTests
    {
        private readonly ITestOutputHelper output;

        public ExamplePerjuryTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void PerjuryTest()
        {
            var testCrime = new Felony
            {
                ActusReus = new Perjury
                {
                    IsFalseTestimony = lp => lp is MarcusWitnessEg,
                    IsJudicialProceeding = lp => lp is MarcusWitnessEg,
                    IsUnderOath = lp => lp is MarcusWitnessEg,
                    //lied about some immaterial stuff
                    IsMaterialIssue = lp => false
                },
                MensRea = new Purposely
                {
                    IsKnowledgeOfWrongdoing = lp => lp is MarcusWitnessEg
                }
            };

            var testResult = testCrime.IsValid(new MarcusWitnessEg());
            this.output.WriteLine(testCrime.ToString());
            Assert.False(testResult);
        }
    }

    public class MarcusWitnessEg : LegalPerson, IDefendant
    {
        public MarcusWitnessEg() : base("MARCUS WITNESS") { }
    }
}
