using System;
using NoFuture.Law.Procedure.Criminal.US;
using NoFuture.Law.Procedure.Criminal.US.Intrusions;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Procedure.Criminal.Tests
{
    public class ExampleFriskTests
    {
        private readonly ITestOutputHelper output;

        public ExampleFriskTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestFriskIsValid00()
        {
            var testSubject = new Frisk
            {
                IsBeliefFreeToGo = lp => false,
                GetDetainedTimespan = lp => new TimeSpan(0, 15, 0),
                GetRequiredInvestigateTimespan = lp => new TimeSpan(0, 20, 0)
            };

            var testResult = testSubject.IsValid(new ExampleSuspect(), new ExampleLawEnforcement());
            Assert.False(testResult);
            this.output.WriteLine(testSubject.ToString());

        }

        [Fact]
        public void TestFriskIsValid01()
        {
            var testSubject = new Frisk
            {
                IsBeliefFreeToGo = lp => false,
                GetDetainedTimespan = lp => new TimeSpan(0, 15, 0),
                GetRequiredInvestigateTimespan = lp => new TimeSpan(0, 20, 0),
                IsBeliefArmedAndDangerous = (lp1, lp2) => 
                    lp1 is ExampleSuspect && lp2 is ExampleLawEnforcement
                    ||
                    lp1 is ExampleLawEnforcement && lp2 is ExampleSuspect
            };

            var testResult = testSubject.IsValid(new ExampleSuspect(), new ExampleLawEnforcement());
            Assert.True(testResult);
            this.output.WriteLine(testSubject.ToString());

        }
    }
}
