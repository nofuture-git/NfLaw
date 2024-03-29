﻿using System;
using Xunit;
using NoFuture.Law.US.Persons;
using NoFuture.Law.Tort.US.UnintentionalTort;
using NoFuture.Law.US;
using Xunit.Abstractions;

namespace NoFuture.Law.Tort.Tests
{
    /// <summary>
    /// Herskovits v. Group Health Coop., 664 P.2d 474 (Wash. 1983)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, illustrate the lost-chance approach
    /// ]]>
    /// </remarks>
    public class HerskovitsvGroupHealthCoopTests
    {
        private readonly ITestOutputHelper output;

        public HerskovitsvGroupHealthCoopTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void HerskovitsvGroupHealthCoop()
        {
            var test = new LostChanceApproach(ExtensionMethods.Tortfeasor)
            {
                FactualCause = new FactualCause(ExtensionMethods.Tortfeasor)
                {
                    IsButForCaused = lp => lp is GroupHealthCoop,
                },
                GetLostProbability = lp => lp is Herskovits ? 0.39D : 1D,
                GetActualProbability = lp => lp is Herskovits ? 0.25D : 1D
            };

            var testResult = test.IsValid(new Herskovits(), new GroupHealthCoop());
            this.output.WriteLine(test.ToString());
            Assert.True(testResult);

        }
    }

    public class Herskovits : LegalPerson, IPlaintiff
    {
        public Herskovits(): base("Herskovits") { }
    }

    public class GroupHealthCoop : LegalPerson, ITortfeasor
    {
        public GroupHealthCoop(): base("Group Health Coop.") { }
    }
}
