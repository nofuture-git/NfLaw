using System;
using NoFuture.Law.Procedure.Criminal.US.Interrogations;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Procedure.Criminal.Tests
{
    public class ExampleVoluntarinessTests
    {
        private readonly ITestOutputHelper output;

        public ExampleVoluntarinessTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestVoluntarinessIsValid00()
        {
            var testSubject = new Voluntariness
            {
                IsSubjectedToCoercivePoliceConduct = lp => lp is ExampleSuspect,
                IsSufficientToOvercomeWillpower = lp => lp is ExampleSuspect
            };

            var testResult = testSubject.IsValid(new ExampleLawEnforcement(), new ExampleSuspect());
            this.output.WriteLine(testSubject.ToString());
            Assert.False(testResult);
        }
    }
}
