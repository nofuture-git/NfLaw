using System;
using System.Linq;
using NoFuture.Law.Procedure.Criminal.US.Interrogations;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Procedure.Criminal.Tests
{
    public class ExampleRightToCounselApproachTests
    {
        private readonly ITestOutputHelper output;

        public ExampleRightToCounselApproachTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestRightToCounselApproachIsValid00()
        {
            var testSubject = new RightToCounselApproach
            {
                IsDeliberateElicit = lp => lp is ExampleLawEnforcement,
                CurrentDateTime =  DateTime.UtcNow.AddDays(2),
                GetDateInitiationJudicialProceedings = lps => lps.Any(lp => lp is ExampleSuspect) ? new DateTime?(DateTime.UtcNow.AddDays(-2)) : null,
            };

            var testResult = testSubject.IsValid(new ExampleSuspect(), new ExampleLawEnforcement());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
        }
    }
}
