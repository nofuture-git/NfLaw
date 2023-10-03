using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.AgainstGov;
using NoFuture.Law.Criminal.US.Elements.Intent.PenalCode;
using NoFuture.Law.US.Persons;
using Xunit;

namespace NoFuture.Law.Criminal.Tests.AgainstGovTests
{
    [TestFixture()]
    public class ExampleObstructionOfJusticeTests
    {
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
            Console.WriteLine(testCrime.ToString());
            Assert.IsTrue(testResult);
        }
    }

    public class BarryBondsEg : LegalPerson, IDefendant
    {
        public BarryBondsEg() : base("BARRY BONDS") {  }
    }
}
