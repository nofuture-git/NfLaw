using System;
using NoFuture.Law.Procedure.Civil.US.Pleadings;
using NoFuture.Law.US.Courts;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Procedure.Civil.Tests
{
    public class ExampleTestOrderJoiner
    {
        private readonly ITestOutputHelper output;

        public ExampleTestOrderJoiner(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestOrderJoinerIsValid()
        {
            var testSubject = new OrderJoiner
            {
                GetAssertion = lp => new ExampleCauseForAction(),
                Court = new StateCourt("CA"),
                GetRequestedRelief = lp => new ExampleRequestRelief(),
                IsSigned = lp => true,
                IsRequiredForCompleteRelief = lp => lp is ExampleAbsentee
            };

            var testResult = testSubject.IsValid(new ExamplePlaintiff(), new ExampleDefendant(), new ExampleAbsentee());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
            testSubject.ClearReasons();

            testSubject.IsRequiredForCompleteRelief = lp => false;
            testSubject.IsRequiredToProtectOthersExposure = lp => lp is ExampleAbsentee;

            testResult = testSubject.IsValid(new ExamplePlaintiff(), new ExampleDefendant(), new ExampleAbsentee());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
            testSubject.ClearReasons();

            testSubject.IsRequiredToProtectOthersExposure = lp => false;
            testSubject.IsRequiredToAvoidContradictoryObligations = lp => lp is ExampleAbsentee;

            testResult = testSubject.IsValid(new ExamplePlaintiff(), new ExampleDefendant(), new ExampleAbsentee());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
            testSubject.ClearReasons();

            testSubject.IsRequiredToAvoidContradictoryObligations = lp => false;
            testResult = testSubject.IsValid(new ExamplePlaintiff(), new ExampleDefendant(), new ExampleAbsentee());
            this.output.WriteLine(testSubject.ToString());
            Assert.False(testResult);
        }
    }
}
