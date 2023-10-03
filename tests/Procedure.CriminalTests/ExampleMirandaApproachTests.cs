using System;
using NoFuture.Law.Procedure.Criminal.US.Interrogations;
using Xunit;

namespace NoFuture.Law.Procedure.Criminal.Tests
{
    public class ExampleMirandaApproachTests
    {
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
            Console.WriteLine(testSubject.ToString());
            Assert.IsTrue(testResult);
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
            Console.WriteLine(testSubject.ToString());
            Assert.IsFalse(testResult);
        }
    }
}
