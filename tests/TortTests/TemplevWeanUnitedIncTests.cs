using System;
using Xunit;
using NoFuture.Law.US.Persons;
using NoFuture.Law.Tort.US.Elements;
using NoFuture.Law.US;
using Xunit.Abstractions;

namespace NoFuture.Law.Tort.Tests
{
    /// <summary>
    /// Temple v. Wean United, Inc., 364 N.E.2d 267 (Ohio 1977)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, product liability requires defect is not a later mod
    /// ]]>
    /// </remarks>
    public class TemplevWeanUnitedIncTests
    {
        private readonly ITestOutputHelper output;

        public TemplevWeanUnitedIncTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TemplevWeanUnitedInc()
        {
            var test = new ProductStrictLiability(ExtensionMethods.Tortfeasor)
            {
                Injury = new Harm(ExtensionMethods.Plaintiff)
                {
                    IsDisability = lp => lp is Temple,
                    IsOfFreedomLost = lp => lp is Temple,
                    IsOfPleasureLost = lp => lp is Temple
                },
                IsDirectCause = p => p is Warco75TonPowerPunchPress,
                //was modified which caused harm
                IsDefectiveAtTimeOfSale = p => false,
                SubjectProperty = new Warco75TonPowerPunchPress()
            };
            var testResult = test.IsValid(new Temple(), new WeanUnitedInc());
            Assert.False(testResult);
            this.output.WriteLine(test.ToString());
        }
    }

    public class Warco75TonPowerPunchPress : LegalProperty
    {
        public Warco75TonPowerPunchPress() : base("Warco 75 ton power punch press") { }

    }

    public class Temple : LegalPerson, IPlaintiff
    {
        public Temple(): base("Temple") { }
    }

    public class WeanUnitedInc : LegalPerson, ITortfeasor
    {
        public WeanUnitedInc(): base("Wean United, Inc.") { }
    }
}
