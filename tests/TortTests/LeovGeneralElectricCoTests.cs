using System;
using Xunit;
using NoFuture.Law.US.Persons;
using NoFuture.Law.Tort.US.IntentionalTort;
using NoFuture.Law.US;
using Xunit.Abstractions;

namespace NoFuture.Law.Tort.Tests
{
    /// <summary>
    /// Leo v. General Electric Co., 538 N.Y.S.2d 844 (N.Y. App. Div. 1989)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, demo's the idea of private peculiar injury since it was a loss suffered only by commercial fishermen on public waters
    /// ]]>
    /// </remarks>
    public class LeovGeneralElectricCoTests
    {
        private readonly ITestOutputHelper output;

        public LeovGeneralElectricCoTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void LeovGeneralElectricCo()
        {
            Predicate<IPlaintiff> isMoneyInjury = lp => lp is Leo;

            var test = new PublicNuisance(ExtensionMethods.Tortfeasor)
            {
                IsProscribedByGovernment = lp => true,
                IsPrivatePeculiarInjury = lp => lp is Leo && isMoneyInjury(lp)
            };

            var testResult = test.IsValid(new Leo(), new GeneralElectricCo());
            Assert.True(testResult);
            this.output.WriteLine(test.ToString());
        }
    }

    public class Leo : LegalPerson, IPlaintiff
    {
        public Leo(): base("Leo") { }
    }

    public class GeneralElectricCo : LegalPerson, ITortfeasor
    {
        public GeneralElectricCo(): base("General Electric Co.") { }
    }
}
