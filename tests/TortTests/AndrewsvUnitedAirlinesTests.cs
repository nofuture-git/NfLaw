using System;
using Xunit;
using NoFuture.Law.US.Persons;
using NoFuture.Law.Tort.US.UnintentionalTort;
using NoFuture.Law.US;
using Xunit.Abstractions;

namespace NoFuture.Law.Tort.Tests
{
    /// <summary>
    /// Andrews v. United Airlines, 24 F.3d 39 (9th Cir. 1994)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, enhanced duty for legal person type of "Common Carriers"
    /// ]]>
    /// </remarks>
    public class AndrewsvUnitedAirlinesTests
    {
        private readonly ITestOutputHelper output;

        public AndrewsvUnitedAirlinesTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void AndrewsvUnitedAirlines()
        {
            var test = new LearnedHandsFormula(ExtensionMethods.Tortfeasor)
            {
                GetRiskOfLoss = lp =>
                {
                    if (!(lp is UnitedAirlines))
                    {
                        return 0d;
                    }

                    return 135d / 175000d;
                },
                GetCostOfLoss = lp =>
                {
                    if (!(lp is UnitedAirlines))
                    {
                        return 0m;
                    }
                    //just a guess
                    return 1000000m;
                },
                GetCostOfPrecaution = lp =>
                {
                    if (!(lp is UnitedAirlines))
                    {
                        return 0m;
                    }

                    var numberOfAirplanes = 175000d / 365d;
                    var costOfAdditionalEquip = 7000d;

                    return Convert.ToDecimal(numberOfAirplanes * costOfAdditionalEquip);
                }
            };

            var calcCost = test.Calculate(new UnitedAirlines());
            this.output.WriteLine(calcCost.ToString());
            test.ClearReasons();
            var testResult = test.IsValid(new UnitedAirlines(), new Andrews());
            Assert.False(testResult);
            this.output.WriteLine(test.ToString());
        }
    }

    public class Andrews : LegalPerson, IPlaintiff
    {
        public Andrews(): base("Andrews") { }
    }

    public class UnitedAirlines : LegalPerson, ITortfeasor
    {
        public UnitedAirlines(): base("United Airlines") { }
    }
}
