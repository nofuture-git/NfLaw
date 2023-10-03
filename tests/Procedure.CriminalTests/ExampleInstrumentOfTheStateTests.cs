using System;
using System.Linq;
using NoFuture.Law.Procedure.Criminal.US;
using NoFuture.Law.Procedure.Criminal.US.Searches;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Procedure.Criminal.Tests
{
    public class ExampleInstrumentOfTheStateTests
    {
        private readonly ITestOutputHelper output;

        public ExampleInstrumentOfTheStateTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestInstrumentOfTheStateIsValid00()
        {
            var testSubject = new InstrumentOfTheState
            {
                GetConductorOfSearch = lps => lps.FirstOrDefault(lp => lp is ExampleCitizenSearchConductor),
                IsAcquiescenceOfTheState = lp => false,
                IsPromoteInterestOfTheState = lp => false
            };
            var testResult = testSubject.IsValid(new ExampleCitizenSearchConductor(), new ExampleLawEnforcement());
            Assert.False(testResult);
            this.output.WriteLine(testSubject.ToString());
        }

        [Fact]
        public void TestInstrumentOfTheStateIsValid01()
        {
            var testSubject = new InstrumentOfTheState
            {
                GetConductorOfSearch = lps => lps.FirstOrDefault(lp => lp is ExampleCitizenSearchConductor),
                IsAcquiescenceOfTheState = lp => true,
                IsPromoteInterestOfTheState = lp => true
            };
            var testResult = testSubject.IsValid(new ExampleCitizenSearchConductor(), new ExampleLawEnforcement());
            Assert.True(testResult);
            this.output.WriteLine(testSubject.ToString());
        }
    }

    public class ExampleCitizenSearchConductor : LegalPerson
    {
        public ExampleCitizenSearchConductor() : base("U.B. Searchin") { }
    }

    public class ExampleLawEnforcement : LegalPerson, ILawEnforcement
    {
        public ExampleLawEnforcement() : base("Johnny Law") { }
    }
}
