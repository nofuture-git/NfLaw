using System;
using NoFuture.Law.Procedure.Criminal.US.Witness;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Procedure.Criminal.Tests
{
    public class ExampleEyewitnessIdentificationTests
    {
        private readonly ITestOutputHelper output;

        public ExampleEyewitnessIdentificationTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestEyewitnessIdentificationIsValid00()
        {
            var testSubject = new EyewitnessIdentification
            {
                IsProcedureSuggestive = lp => lp is ExampleSuspect,
                IsProcedureUnnecessary = lp => lp is ExampleSuspect,
                IsProcedureUnreliable = lp => lp is ExampleSuspect
            };

            var testResult = testSubject.IsValid(new ExampleLawEnforcement(), new ExampleSuspect());
            this.output.WriteLine(testSubject.ToString());
            Assert.False(testResult);

        }
    }
}
