using System;
using NoFuture.Law.Criminal.US.Elements.AgainstProperty.Theft;
using NoFuture.Law.Criminal.US.Elements.AttendantCircumstances;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.TheftTests
{
    public class ExampleRobberyTests
    {
        private readonly ITestOutputHelper output;

        public ExampleRobberyTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ExampleRobberyActTest()
        {
            var testAct = new Robbery
            {
                Consent = new VictimConsent
                {
                    IsApprovalExpressed = lp => false,
                    IsCapableThereof = lp => lp is LindseyDealinEg
                },
                IsTakenPossession = lp => lp is RodneyBlackmailEg,
                IsAsportation = lp => lp is RodneyBlackmailEg,
                IsByViolence = lp => lp is RodneyBlackmailEg,
                SubjectProperty = new LegalProperty("money")
                    {IsEntitledTo = lp => lp is LindseyDealinEg, IsInPossessionOf = lp => lp is LindseyDealinEg, PropertyValue = dt => 15000m },
                
            };

            var testResult = testAct.IsValid(new RodneyBlackmailEg(), new LindseyDealinEg());
            this.output.WriteLine(testAct.ToString());
            Assert.True(testResult);
        }
    }
}
