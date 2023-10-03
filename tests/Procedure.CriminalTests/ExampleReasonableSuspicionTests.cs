using System;
using System.Linq;
using NoFuture.Law.Procedure.Criminal.US;
using NoFuture.Law.Procedure.Criminal.US.Intrusions;
using NoFuture.Law.Procedure.Criminal.US.SearchReasons;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Procedure.Criminal.Tests
{
    public class ExampleReasonableSuspicionTests
    {
        private readonly ITestOutputHelper output;

        public ExampleReasonableSuspicionTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestReasonableSuspicionIsValid00()
        {
            var testSubject = new ReasonableSuspicion
            {
                IsFactsConcludeToCriminalActivity = lp => lp is ExampleLawEnforcement,
                Stop = new SuspectStop
                {
                    IsBeliefFreeToGo = lp => false,
                    GetDetainedTimespan = lp => new TimeSpan(0, 15, 0),
                    GetRequiredInvestigateTimespan = lp => new TimeSpan(0, 20, 0)
                }
            };

            var testResult =
                testSubject.IsValid(new ExampleSuspect(), new ExampleLawEnforcement(), new ExampleInformant());
            Assert.True(testResult);
            this.output.WriteLine(testSubject.ToString());
        }

        [Fact]
        public void TestReasonableSuspicionIsValid01()
        {
            var testSubject = new ReasonableSuspicion
            {
                GetInformationSource = lps => lps.FirstOrDefault(lp => lp is IInformant),
                IsFactsConcludeToCriminalActivity = lp => lp is ExampleLawEnforcement,
                Stop = new SuspectStop
                {
                    IsBeliefFreeToGo = lp => false,
                    GetDetainedTimespan = lp => new TimeSpan(0, 15, 0),
                    GetRequiredInvestigateTimespan = lp => new TimeSpan(0, 20, 0)
                }
            };

            var testResult =
                testSubject.IsValid(new ExampleSuspect(), new ExampleLawEnforcement(),
                    new ExampleInformant {IsInformationSufficientlyReliable = true});
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
        }

        [Fact]
        public void TestReasonableSuspicionIsValid02()
        {
            var testSubject = new ReasonableSuspicion
            {
                GetInformationSource = lps => lps.FirstOrDefault(lp => lp is ExampleLawEnforcement),
                IsFactsConcludeToCriminalActivity = lp => lp is ExampleLawEnforcement,
                Stop = new SuspectStop
                {
                    IsBeliefFreeToGo = lp => false,
                    GetDetainedTimespan = lp => new TimeSpan(3, 0, 0),
                    GetRequiredInvestigateTimespan = lp => new TimeSpan(0, 20, 0)
                }
            };

            var testResult =
                testSubject.IsValid(new ExampleSuspect(), new ExampleLawEnforcement(),
                    new ExampleInformant { IsInformationSufficientlyReliable = true });
            this.output.WriteLine(testSubject.ToString());
            Assert.False(testResult);
        }
    }
}
