using System;
using Xunit;
using NoFuture.Law.US.Persons;
using NoFuture.Law.Tort.US.UnintentionalTort;
using NoFuture.Law.US;
using Xunit.Abstractions;

namespace NoFuture.Law.Tort.Tests
{
    /// <summary>
    /// Byrne v. Boadle, 159 Eng. Rep. 299 (Court of Exchequer and Exchequer Chamber, 1863)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, negligence being the very fact that such-and-such happened at all
    /// ]]>
    /// </remarks>
    public class ByrnevBoadleTests
    {
        private readonly ITestOutputHelper output;

        public ByrnevBoadleTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ByrnevBoadle()
        {
            var test = new Negligence(ExtensionMethods.Tortfeasor)
            {
                Causation = new Causation(ExtensionMethods.Tortfeasor)
                {
                    FactualCause = new FactualCause(ExtensionMethods.Tortfeasor)
                    {
                        IsButForCaused = lp => lp is Boadle
                    },
                    ProximateCause = new ProximateCause(ExtensionMethods.Tortfeasor)
                    {
                        IsForeseeable = lp => lp.ResIpsaLoquitur()
                    }
                }
            };
            var testResult = test.IsValid(new Byrne(), new Boadle());
            Assert.True(testResult);

            this.output.WriteLine(test.ToString());
        }
    }

    public class Byrne : LegalPerson, IPlaintiff
    {
        public Byrne(): base("Byrne") { }
    }

    public class Boadle : LegalPerson, ITortfeasor
    {
        public Boadle(): base("Boadle") { }
    }
}
