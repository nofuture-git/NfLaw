using System;
using System.Linq;
using NoFuture.Law.Procedure.Criminal.US;
using NoFuture.Law.Procedure.Criminal.US.Searches;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Procedure.Criminal.Tests
{
    public class ExampleSearchTests
    {
        private readonly ITestOutputHelper output;

        public ExampleSearchTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestSearchIsValid00()
        {
            var testSubject = new Search
            {
                GetConductorOfSearch = lps => lps.FirstOrDefault(lp => lp is ExampleLawEnforcement),
                ExpectationOfPrivacy = new ExpectationOfPrivacy
                {
                    GetSubjectOfSearch = lps => lps.FirstOrDefault(lp => lp is ExamplePersonSearched),
                    IsPrivacyExpected = lp => lp is ExamplePersonSearched,
                    IsPrivacyExpectedReasonable = lp => lp is ExamplePersonSearched
                }
            };

            var testResult = testSubject.IsValid(new ExamplePersonSearched(), new ExampleLawEnforcement());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
        }

        [Fact]
        public void TestSearchIsValid01()
        {
            var testSubject = new Search
            {
                GetConductorOfSearch = lps => lps.FirstOrDefault(lp => lp is ExampleLawEnforcement),
                ExpectationOfPrivacy = new ExpectationOfPrivacy
                {
                    GetSubjectOfSearch = lps => lps.FirstOrDefault(lp => lp is ExamplePersonSearched),
                    Consent = Consent.IsGiven(),
                    IsPrivacyExpected = lp => false,
                    IsPrivacyExpectedReasonable = lp => false
                }
            };

            var testResult = testSubject.IsValid(new ExamplePersonSearched(), new ExampleLawEnforcement());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
        }
    }

    public class ExamplePersonSearched : LegalPerson, IDefendant
    {
        public ExamplePersonSearched() : base("I.B. Ingsearched") { }
    }
}
