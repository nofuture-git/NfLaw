using System;
using NoFuture.Law.Procedure.Civil.US.Discovery;
using NoFuture.Law.US;
using NoFuture.Law.US.Courts;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Procedure.Civil.Tests.DiscoveryTests
{
    public class ExampleTestRequestForProduction
    {
        private readonly ITestOutputHelper output;

        public ExampleTestRequestForProduction(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestRequestForProductionIsValid00()
        {
            var testSubject = new RequestForProduction
            {
                Court = new StateCourt("AL"),
                GetSubjectPerson = lps => lps.Plaintiff(),
                GetAssertion = lp => lp is IPlaintiff ? new ExampleCauseForAction() : null,
                IsLimitedByCourtOrder = lc => lc is ExampleCauseForAction
            };

            var testResult = testSubject.IsValid(new ExamplePlaintiff(), new ExampleDefendant());
            this.output.WriteLine(testSubject.ToString());
            Assert.False(testResult);

        }

        [Fact]
        public void TestRequestForProductionIsValid01()
        {
            var testSubject = new RequestForProduction
            {
                Court = new StateCourt("AL"),
                GetSubjectPerson = lps => lps.Plaintiff(),
                GetAssertion = lp => lp is IPlaintiff ? new ExampleCauseForAction() : null,
                IsIrrelevantToPartyClaimOrDefense = (lp, lc) => lc is ExampleCauseForAction
            };

            var testResult = testSubject.IsValid(new ExamplePlaintiff(), new ExampleDefendant());
            this.output.WriteLine(testSubject.ToString());
            Assert.False(testResult);

        }

        [Fact]
        public void TestRequestForProductionIsValid02()
        {
            var testSubject = new RequestForProduction
            {
                Court = new StateCourt("AL"),
                GetSubjectPerson = lps => lps.Plaintiff(),
                GetAssertion = lp => lp is IPlaintiff ? new ExampleCauseForAction() : null,
                IsPrivilegedMatter = lc => lc is ExampleCauseForAction
            };

            var testResult = testSubject.IsValid(new ExamplePlaintiff(), new ExampleDefendant());
            this.output.WriteLine(testSubject.ToString());
            Assert.False(testResult);

        }

        [Fact]
        public void TestRequestForProductionIsValid03()
        {
            var testSubject = new RequestForProduction
            {
                Court = new StateCourt("AL"),
                GetSubjectPerson = lps => lps.Plaintiff(),
                GetAssertion = lp => lp is IPlaintiff ? new ExampleCauseForAction() : null,
                IsUnbalancedToNeedsOfCase = lc => lc is ExampleCauseForAction
            };

            var testResult = testSubject.IsValid(new ExamplePlaintiff(), new ExampleDefendant());
            this.output.WriteLine(testSubject.ToString());
            Assert.False(testResult);

        }

        [Fact]
        public void TestRequestForProductionIsValid04()
        {
            var testSubject = new RequestForProduction
            {
                Court = new StateCourt("AL"),
                GetSubjectPerson = lps => lps.Plaintiff(),
                GetAssertion = lp => lp is IPlaintiff ? new ExampleCauseForAction() : null,
                IsReasonablyAccessible = lc => !(lc is ExampleCauseForAction)
            };

            var testResult = testSubject.IsValid(new ExamplePlaintiff(), new ExampleDefendant());
            this.output.WriteLine(testSubject.ToString());
            Assert.False(testResult);

        }
    }
}
