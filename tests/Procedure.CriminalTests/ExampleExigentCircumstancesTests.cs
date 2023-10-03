using System;
using NoFuture.Law.Procedure.Criminal.US.Intrusions;
using NoFuture.Law.Procedure.Criminal.US.SearchReasons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Procedure.Criminal.Tests
{
    public class ExampleExigentCircumstancesTests
    {
        private readonly ITestOutputHelper output;

        public ExampleExigentCircumstancesTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestExigentCircumstancesIsValid00()
        {
            var testSubject = new Arrest
            {
                IsAwareOfBeingArrested = lp => true,
                ProbableCause = new ExampleExigentCircumstances(),
                IsOccurInPublicPlace = lp => false,
            };

            var testResult = testSubject.IsValid(new ExampleSuspect(), new ExampleLawEnforcement());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
        }
    }

    public class ExampleExigentCircumstances : ExigentCircumstances
    {
        public override bool IsValid(params ILegalPerson[] persons)
        {
            return true;
        }
    }
}
