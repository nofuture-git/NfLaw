using System;
using System.Linq;
using NoFuture.Law.Procedure.Criminal.US;
using NoFuture.Law.Procedure.Criminal.US.Searches;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Procedure.Criminal.Tests
{
    public class ExampleMobileVehicleSearchTests
    {
        private readonly ITestOutputHelper output;

        public ExampleMobileVehicleSearchTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestMobileVehicleSearchIsValid00()
        {
            IMobileVehicleSearch testSubject = new MobileVehicleSearch
            {
                GetConductorOfSearch = lps => lps.FirstOrDefault(lp => lp is ExampleLawEnforcement),
                IsBeliefEvidenceToCrimeIsPresent = lp => lp is ExampleLawEnforcement
            };

            var testResult = testSubject.IsValid(new ExampleSuspect(), new ExampleLawEnforcement());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
        }
    }
}
