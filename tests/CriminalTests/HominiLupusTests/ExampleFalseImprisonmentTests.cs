using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.AgainstPersons;
using NoFuture.Law.Criminal.US.Elements.Intent.ComLaw;
using Xunit;

namespace NoFuture.Law.Criminal.Tests.HominiLupusTests
{
    
    public class ExampleFalseImprisonmentTests
    {
        [Fact]
        public void ExampleIsFalseImprisonment()
        {
            var testCrime = new Felony
            {
                ActusReus = new FalseImprisonment
                {
                    IsConfineVictim = lp => lp is ThomasHitchhikerEg
                },
                MensRea = new GeneralIntent
                {
                    IsKnowledgeOfWrongdoing = lp => lp is ThomasHitchhikerEg
                }
            };

            var testResult = testCrime.IsValid(new ThomasHitchhikerEg());
            Console.WriteLine(testCrime.ToString());
            Assert.IsTrue(testResult);
        }
    }
}
