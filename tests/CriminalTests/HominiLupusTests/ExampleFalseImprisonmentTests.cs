using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.AgainstPersons;
using NoFuture.Law.Criminal.US.Elements.Intent.ComLaw;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.HominiLupusTests
{
    public class ExampleFalseImprisonmentTests
    {
        private readonly ITestOutputHelper output;

        public ExampleFalseImprisonmentTests(ITestOutputHelper output)
        {
            this.output = output;
        }

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
            this.output.WriteLine(testCrime.ToString());
            Assert.True(testResult);
        }
    }
}
