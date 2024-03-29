﻿using System;
using Xunit;
using NoFuture.Law.US.Persons;
using NoFuture.Law.Tort.US.UnintentionalTort;
using NoFuture.Law.US;
using Xunit.Abstractions;

namespace NoFuture.Law.Tort.Tests
{
    /// <summary>
    /// Murphy v. Steeplechase Amusement Co., 250 N.Y. 479 (N.Y. 1929)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, plaintiff understood risk, to continue means they contributed 
    /// ]]>
    /// </remarks>
    public class MurphyvSteeplechaseAmusementCoTests
    {
        private readonly ITestOutputHelper output;

        public MurphyvSteeplechaseAmusementCoTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void MurphyvSteeplechaseAmusementCo()
        {
            var test = new ContributoryNegligence<AmusementRide>(ExtensionMethods.Tortfeasor)
            {
                GetContribution = lp =>
                {
                    if (lp is SteeplechaseAmusementCo)
                        return new AmusementRide(1);
                    if (lp is Murphy)
                        return new AmusementRide(1);
                    return new AmusementRide(0);
                }
            };

            var testResult = test.IsValid(new Murphy(), new SteeplechaseAmusementCo());
            Assert.False(testResult);

            this.output.WriteLine(test.ToString());
        }
    }

    public class Murphy : LegalPerson, IPlaintiff
    {
        public Murphy(): base("Murphy") { }
    }

    public class SteeplechaseAmusementCo : LegalPerson, ITortfeasor
    {
        public SteeplechaseAmusementCo(): base("Steeplechase Amusement Co.") { }
    }

    public class AmusementRide : IRankable
    {
        private readonly int _rank;

        public AmusementRide(int rank)
        {
            _rank = rank;
        }

        public int GetRank()
        {
            return _rank;
        }
    }
}
