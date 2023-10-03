using System;
using Xunit;
using NoFuture.Law.US.Persons;
using NoFuture.Law.Tort.US.Terms;
using NoFuture.Law.Tort.US.UnintentionalTort;
using NoFuture.Law.US;
using Xunit.Abstractions;

namespace NoFuture.Law.Tort.Tests
{
    /// <summary>
    /// Trimarco v. Klein, 436 N.E.2d 502 (N.Y. 1982)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, sorting that a thing is a custom and that it is, indeed, breached
    /// ]]>
    /// </remarks>
    public class TrimarcovKleinTests
    {
        private readonly ITestOutputHelper output;

        public TrimarcovKleinTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TrimarcovKlein()
        {
            var test = new Negligence(ExtensionMethods.Tortfeasor)
            {
                SafetyConvention = new ReplaceWithShatterproofGlass()
                {
                    IsConformedTo = lp => !(lp is Klein)
                },
                Causation = new Causation(ExtensionMethods.Tortfeasor)
                {
                    FactualCause = new FactualCause(ExtensionMethods.Tortfeasor)
                    {
                        IsButForCaused = lp => lp is Klein
                    },
                    ProximateCause = new ProximateCause(ExtensionMethods.Tortfeasor)
                    {
                        IsForeseeable = lp => true
                    }
                }
            };

            var testResult = test.IsValid(new Trimarco(), new Klein());
            Assert.True(testResult);

            this.output.WriteLine(test.ToString());
        }
    }

    public class Trimarco : LegalPerson, IPlaintiff
    {
        public Trimarco(): base("Trimarco") { }
    }

    public class Klein : LegalPerson, ITortfeasor
    {
        public Klein(): base("Klein") { }
    }

    public class ReplaceWithShatterproofGlass : CustomsTerm
    {

    }
}
