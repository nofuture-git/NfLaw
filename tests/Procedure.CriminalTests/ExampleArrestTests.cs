using System;
using System.Linq;
using NoFuture.Law.Procedure.Criminal.US.Intrusions;
using NoFuture.Law.Procedure.Criminal.US.Warrants;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Procedure.Criminal.Tests
{
    public class ExampleArrestTests
    {
        private readonly ITestOutputHelper output;

        public ExampleArrestTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestArrestIsValid00()
        {
            var testSubject = new Arrest
            {
                IsAwareOfBeingArrested = lp => true,
                ProbableCause = new ExampleExigentCircumstances(),
                IsOccurInPublicPlace = lp => true
            };

            var testResult = testSubject.IsValid(new ExampleSuspect(), new ExampleLawEnforcement());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
        }

        [Fact]
        public void TestArrestIsValid01()
        {
            var testSubject = new Arrest
            {
                IsAwareOfBeingArrested = lp => true,
                ProbableCause = new ExampleProbableCause(),
                IsOccurInPublicPlace = lp => false,
                Warrant = new ArrestWarrant
                {
                    GetObjectiveOfSearch = () => new ExampleSuspect(),
                    IsObjectiveDescribedWithParticularity = lp => lp is ExampleSuspect,
                    IsIssuerNeutralAndDetached = lp => lp is ExampleJudge,
                    IsIssuerCapableDetermineProbableCause = lp => lp is ExampleJudge,
                    GetIssuerOfWarrant = lps => lps.FirstOrDefault(lp => lp is IJudge)
                }
            };

            var testResult = testSubject.IsValid(new ExampleSuspect(), new ExampleLawEnforcement(), new ExampleJudge());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
        }


       
    }
}
