using System;
using NoFuture.Law.Procedure.Criminal.US.Interrogations;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Procedure.Criminal.Tests
{
    public class ExampleMirandaApproachTests
    {
        private readonly ITestOutputHelper output;

        public ExampleMirandaApproachTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestMirandaApproachIsValid00()
        {
            var testSubject = new MirandaApproach
            {
                IsInCoercivePressureEnvironment = lp => lp is ExampleSuspect,
                IsIncriminatoryQuestioning = lp => lp is ExampleSuspect, 
                IsToldRightToRemainSilent = lp => lp is ExampleSuspect,
                IsToldRightToAttorney = lp => lp is ExampleSuspect
            };

            var testResult = testSubject.IsValid(new ExampleLawEnforcement(), new ExampleSuspect());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
        }

        [Fact]
        public void TestMirandaApproachIsValid01()
        {
            var testSubject = new MirandaApproach
            {
                IsInCoercivePressureEnvironment = lp => lp is ExampleSuspect,
                //is voluntary
                IsIncriminatoryQuestioning = lp => false,
                IsToldRightToRemainSilent = lp => lp is ExampleSuspect,
                IsToldRightToAttorney = lp => lp is ExampleSuspect
            };

            var testResult = testSubject.IsValid(new ExampleLawEnforcement(), new ExampleSuspect());
            this.output.WriteLine(testSubject.ToString());
            Assert.False(testResult);
        }
    }
}
