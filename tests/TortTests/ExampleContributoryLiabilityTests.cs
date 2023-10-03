using System;
using NoFuture.Law.Tort.US.Elements;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Tort.Tests
{
    public class ExampleContributoryLiabilityTests
    {
        private readonly ITestOutputHelper output;

        public ExampleContributoryLiabilityTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestContributoryLiabilityIsValid()
        {
            var test = new ContributoryLiability(ExtensionMethods.Defendant)
            {
                IsAction = lp => lp is SomeDefendant,
                IsVoluntary = lp => lp is SomeDefendant,
                IsMutuallyBeneficialRelationship = (l1, l2) =>
                    l1 is Some3rdParty && l2 is SomeDefendant || l1 is SomeDefendant && l2 is Some3rdParty,
                IsEncourageOthersAct = lp => lp is SomeDefendant
            };

            var testResult = test.IsValid(new SomePlaintiff(), new SomeDefendant(), new Some3rdParty());
            Assert.True(testResult);

            this.output.WriteLine(test.ToString());
        }
    }

    public class Some3rdParty : LegalPerson, IThirdParty
    {
        public Some3rdParty() : base("Sonny T. Party III") { }
    }
}
