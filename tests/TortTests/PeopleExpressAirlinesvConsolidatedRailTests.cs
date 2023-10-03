using System;
using Xunit;
using NoFuture.Law.US.Persons;
using NoFuture.Law.Tort.US.UnintentionalTort;
using NoFuture.Law.US;
using Xunit.Abstractions;

namespace NoFuture.Law.Tort.Tests
{
    /// <summary>
    /// People Express Airlines, Inc. v. Consolidated Rail Corporation, 100 N.J. 246 (1985)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, the predicates for econ loss negligence
    /// ]]>
    /// </remarks>
    public class PeopleExpressAirlinesvConsolidatedRailTests
    {
        private readonly ITestOutputHelper output;

        public PeopleExpressAirlinesvConsolidatedRailTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void PeopleExpressAirlinesvConsolidatedRail()
        {
            var test = new EconomicLoss(ExtensionMethods.Tortfeasor)
            {
                Causation = new Causation(ExtensionMethods.Tortfeasor)
                {
                    FactualCause = new FactualCause(ExtensionMethods.Tortfeasor)
                    {
                        IsButForCaused = lp => lp is ConsolidatedRailCorporation
                    },
                    ProximateCause = new ProximateCause(ExtensionMethods.Tortfeasor)
                    {
                        IsForeseeable = lp => lp is ConsolidatedRailCorporation,
                        IsDirectCause = lp => lp is ConsolidatedRailCorporation
                    }
                },
                IsEntityTypeIdentifiable = lp => lp is PeopleExpressAirlinesInc,
                IsCountOfEntityPredictable = lp => lp is PeopleExpressAirlinesInc,
                IsEconActivityIdentifiable =  lp => lp is PeopleExpressAirlinesInc,
                IsLocationOfEntityPredictable = lp => lp is PeopleExpressAirlinesInc,
            };
            var testResult = test.IsValid(new PeopleExpressAirlinesInc(), new ConsolidatedRailCorporation());
            Assert.True(testResult);
            this.output.WriteLine(test.ToString());
        }
    }

    public class PeopleExpressAirlinesInc : LegalPerson, IPlaintiff
    {
        public PeopleExpressAirlinesInc(): base("People Express Airlines, Inc.") { }
    }

    public class ConsolidatedRailCorporation : LegalPerson, ITortfeasor
    {
        public ConsolidatedRailCorporation(): base("Consolidated Rail Corporation") { }
    }
}
