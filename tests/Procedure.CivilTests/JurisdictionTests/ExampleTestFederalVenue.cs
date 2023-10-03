using System;
using NoFuture.Law;
using NoFuture.Law.Procedure.Civil.US.Jurisdiction;
using NoFuture.Law.US.Courts;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Procedure.Civil.Tests
{
    public class ExampleTestFederalVenue
    {
        private readonly ITestOutputHelper output;

        public ExampleTestFederalVenue(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestFederalVenueIsValid()
        {
            var testSubject = new FederalVenue(new FederalCourt("District 01"))
            {
                GetDomicileLocation = lp =>
                {
                    if (lp is IPlaintiff)
                        return new VocaBase("District 02");
                    if (lp is IDefendant)
                        return new VocaBase("District 08");
                    return null;
                },

                GetInjuryLocation = lp => new VocaBase("District 01"),

            };

            var testResult = testSubject.IsValid(new ExamplePlaintiff(), new ExampleDefendant());
            Assert.True(testResult);
            this.output.WriteLine(testSubject.ToString());
        }
    }
}
