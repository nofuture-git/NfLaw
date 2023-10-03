using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.AgainstGov;
using NoFuture.Law.Criminal.US.Elements.Intent.PenalCode;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.AgainstGovTests
{
    public class ExampleObstructionOfJusticeTests
    {
        private readonly ITestOutputHelper output;

        public ExampleObstructionOfJusticeTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestObstructionOfJustice()
        {
            var testCrime = new Misdemeanor
            {
                ActusReus = new ObstructionOfJustice
                {
                    IsRefuseToGiveEvidence = lp => lp is BarryBondsEg
                },
                MensRea = new Purposely
                {
                    IsKnowledgeOfWrongdoing = lp => lp is BarryBondsEg
                }
            };

            var testResult = testCrime.IsValid(new BarryBondsEg());
            this.output.WriteLine(testCrime.ToString());
            Assert.True(testResult);
        }
    }

    public class BarryBondsEg : LegalPerson, IDefendant
    {
        public BarryBondsEg() : base("BARRY BONDS") {  }
    }
}
