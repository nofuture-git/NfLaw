using System;
using System.Linq;
using NoFuture.Law.Procedure.Criminal.US;
using NoFuture.Law.Procedure.Criminal.US.Intrusions;
using NoFuture.Law.Procedure.Criminal.US.Searches;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Procedure.Criminal.Tests
{
    public class ExampleSearchIncidentToArrestTests
    {
        private readonly ITestOutputHelper output;

        public ExampleSearchIncidentToArrestTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestSearchIncidentToArrestIsValid00()
        {
            var testSubject = new Search
            {
               
                ExpectationOfPrivacy = new SearchIncidentToArrest
                {
                    Arrest = new Arrest
                    {
                        IsAwareOfBeingArrested = lp => true,
                        ProbableCause = new ExampleProbableCause(),
                        IsOccurInPublicPlace = lp => true
                    },
                    IsSearchArrestedPerson = lp => lp is ExampleSuspect,
                    GetSubjectOfSearch = lps => lps.FirstOrDefault(lp => lp is ExampleSuspect)
                },
                InstrumentOfTheState = new InstrumentOfTheState
                {
                    IsAgentOfTheState = lp => true
                },
                GetConductorOfSearch = lps => lps.FirstOrDefault(lp => lp is ExampleLawEnforcement),
                
            };

            var testResult = testSubject.IsValid(new ExampleLawEnforcement(), new ExampleSuspect());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
        }

        [Fact]
        public void TestSearchIncidentToArrestIsValid01()
        {
            var testSubject = new Search
            {

                ExpectationOfPrivacy = new SearchIncidentToArrestMotorVehicle()
                {
                    Arrest = new Arrest
                    {
                        IsAwareOfBeingArrested = lp => true,
                        ProbableCause = new ExampleProbableCause(),
                        IsOccurInPublicPlace = lp => true
                    },
                    IsArresteeNearPassengerCompartment = lp => lp is ExampleLawEnforcement,
                    IsArresteeUnsecured = lp => lp is ExampleLawEnforcement,
                    GetSubjectOfSearch = lps => lps.FirstOrDefault(lp => lp is ExampleSuspect),
                },
                InstrumentOfTheState = new InstrumentOfTheState
                {
                    IsAgentOfTheState = lp => true
                },
                GetConductorOfSearch = lps => lps.FirstOrDefault(lp => lp is ExampleLawEnforcement),

            };

            var testResult = testSubject.IsValid(new ExampleLawEnforcement(), new ExampleSuspect());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
        }

        [Fact]
        public void TestSearchIncidentToArrestIsValid02()
        {
            var testSubject = new Search
            {

                ExpectationOfPrivacy = new SearchIncidentToArrestMotorVehicle()
                {
                    Arrest = new Arrest
                    {
                        IsAwareOfBeingArrested = lp => true,
                        ProbableCause = new ExampleProbableCause(),
                        IsOccurInPublicPlace = lp => true
                    },
                    IsBeliefEvidenceToCrimeIsPresent = lp => lp is ExampleLawEnforcement,
                    GetSubjectOfSearch = lps => lps.FirstOrDefault(lp => lp is ExampleSuspect)
                },
                InstrumentOfTheState = new InstrumentOfTheState
                {
                    IsAgentOfTheState = lp => true
                },
                GetConductorOfSearch = lps => lps.FirstOrDefault(lp => lp is ExampleLawEnforcement),

            };

            var testResult = testSubject.IsValid(new ExampleLawEnforcement(), new ExampleSuspect());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
        }
    }
}
