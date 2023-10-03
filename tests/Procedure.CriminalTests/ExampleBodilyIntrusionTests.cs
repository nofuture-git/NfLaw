using System;
using System.Linq;
using NoFuture.Law.Procedure.Criminal.US.Intrusions;
using NoFuture.Law.Procedure.Criminal.US.Warrants;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Procedure.Criminal.Tests
{
    public class ExampleBodilyIntrusionTests
    {
        private readonly ITestOutputHelper output;

        public ExampleBodilyIntrusionTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestBodilyIntrusionIsValid00()
        {
            var testSubject = new BodilyIntrusion
            {
                ProbableCause = new ExampleExigentCircumstances(),
            };

            var testResult = testSubject.IsValid(new ExampleLawEnforcement(), new ExampleSuspect());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
        }

        [Fact]
        public void TestBodilyIntrusionIsValid01()
        {
            var testSubject = new BodilyIntrusion
            {
                Warrant = new ArrestWarrant
                {
                    GetObjectiveOfSearch = () => new ExampleSuspect(),
                    IsObjectiveDescribedWithParticularity = lp => lp is ExampleSuspect,
                    GetIssuerOfWarrant = lps => lps.FirstOrDefault(lp => lp is ExampleJudge),
                    IsIssuerCapableDetermineProbableCause = lp => lp is ExampleJudge,
                    IsIssuerNeutralAndDetached = lp => lp is ExampleJudge,
                    ProbableCause = new ExampleProbableCause()
                }
            };

            var testResult = testSubject.IsValid(new ExampleLawEnforcement(), new ExampleSuspect(), new ExampleJudge());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
        }

        [Fact]
        public void TestBodilyIntrusionIsValid02()
        {
            var testSubject = new BodilyIntrusion
            {
                IsShockingToConscience = lp => lp is ExampleSuspect,
                Warrant = new ArrestWarrant
                {
                    GetObjectiveOfSearch = () => new ExampleSuspect(),
                    IsObjectiveDescribedWithParticularity = lp => lp is ExampleSuspect,
                    GetIssuerOfWarrant = lps => lps.FirstOrDefault(lp => lp is ExampleJudge),
                    IsIssuerCapableDetermineProbableCause = lp => lp is ExampleJudge,
                    IsIssuerNeutralAndDetached = lp => lp is ExampleJudge,
                    ProbableCause = new ExampleProbableCause()
                }
            };

            var testResult = testSubject.IsValid(new ExampleLawEnforcement(), new ExampleSuspect(), new ExampleJudge());
            this.output.WriteLine(testSubject.ToString());
            Assert.False(testResult);
        }
    }
}
