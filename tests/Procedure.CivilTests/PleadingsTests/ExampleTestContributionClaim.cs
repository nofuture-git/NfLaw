using System;
using NoFuture.Law.Procedure.Civil.US.Pleadings;
using NoFuture.Law.US.Courts;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Procedure.Civil.Tests
{
    public class ExampleTestContributionClaim
    {
        private readonly ITestOutputHelper output;

        public ExampleTestContributionClaim(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestContributionClaimIsValid()
        {
            var testSubject = new ContributionClaim()
            {
                GetAssertion = lp => new ExampleCauseForAction(),
                GetRequestedRelief = lp => new ExampleRequestRelief(),
                IsSigned = lp => true,
                IsShareInLiability = lp => lp is ExampleThirdParty,
                Court = new StateCourt("MN")
            };

            var testResult =
                testSubject.IsValid(new ExamplePlaintiff(), new ExampleDefendant(), new ExampleThirdParty());
            Assert.True(testResult);
            this.output.WriteLine(testSubject.ToString());
        }

    }

    

    public class ExampleThirdParty : LegalPerson, IThirdParty
    {
        public ExampleThirdParty() : base("Sir Party III") { }
    }

    public class ExampleCauseForAction : LegalConcept
    {
        public override bool IsValid(params ILegalPerson[] persons)
        {
            return true;
        }
    }

    public class ExampleRequestRelief : LegalConcept
    {
        public override bool IsValid(params ILegalPerson[] persons)
        {
            return true;
        }
    }
}
