using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.AgainstPublic;
using NoFuture.Law.Criminal.US.Elements.Intent.PenalCode;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.AgainstPublicTests
{
    public class ExampleLoiteringTests
    {
        private readonly ITestOutputHelper output;

        public ExampleLoiteringTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestLoiteringIsValid()
        {
            var testCrime = new Misdemeanor
            {
                ActusReus = new Loitering
                {
                    IsBegging = lp => lp is SomeBumEg,
                },
                MensRea = new Purposely
                {
                    IsIntentOnWrongdoing = lp => lp is SomeBumEg
                }
            };

            var testResult = testCrime.IsValid(new SomeBumEg());
            this.output.WriteLine(testCrime.ToString());
            Assert.True(testResult);
        }
    }

    public class SomeBumEg : LegalPerson, IDefendant
    {
        public SomeBumEg() : base("ARRRAGHH! GRINPNKLOOP...") { }
    }
}
