using System;
using NoFuture.Law;
using NoFuture.Law.Procedure.Criminal.US.Intrusions;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Procedure.Criminal.Tests
{
    public class ExamplePlainViewSeizureTests
    {
        private readonly ITestOutputHelper output;

        public ExamplePlainViewSeizureTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestPlainViewSeizureIsValid00()
        {
            var testSubject = new PlainViewSeizure
            {
                OriginalIntrusion = new Frisk
                {
                    IsBeliefFreeToGo = lp => false,
                    GetDetainedTimespan = lp => new TimeSpan(0, 15, 0),
                    GetRequiredInvestigateTimespan = lp => new TimeSpan(0, 20, 0),
                    IsBeliefArmedAndDangerous = (lp1, lp2) =>
                        lp1 is ExampleSuspect && lp2 is ExampleLawEnforcement
                        ||
                        lp1 is ExampleLawEnforcement && lp2 is ExampleSuspect
                },
              GetObjectOfSeizure = lp => lp is ExampleSuspect ? new ExampleContraband() : null,
              IsObservedInPermissibleScope = lp => lp is ExampleLawEnforcement,
              IsPlainlyApparentClearlyIllegal = i => i is ExampleContraband
            };

            var testResult = testSubject.IsValid(new ExampleSuspect(), new ExampleLawEnforcement());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
        }
    }

    public class ExampleContraband : VocaBase
    {
        public ExampleContraband(): base("Mare'ij'ah'wana") { }
    }
}
