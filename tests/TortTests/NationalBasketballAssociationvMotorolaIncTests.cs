﻿using System;
using System.Collections.Generic;
using System.Linq;
using NoFuture.Law.Property.US.FormsOf;
using NoFuture.Law.Tort.US.IntentionalTort;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Tort.Tests
{
    /// <summary>
    /// National Basketball Association v. Motorola, Inc., 105 F.3d 841
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, misappropriation requires somekind of loss in competitive edge 
    /// ]]>
    /// </remarks>
    public class NationalBasketballAssociationvMotorolaIncTests
    {
        private readonly ITestOutputHelper output;

        public NationalBasketballAssociationvMotorolaIncTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void NationalBasketballAssociationvMotorolaInc()
        {
            var test = new InfoMisappropriation(ExtensionMethods.Tortfeasor)
            {
                SubjectProperty = new NbaGameScores(),
                CalcInformationCost = lp => lp is NationalBasketballAssociation ? 1000m : 0m,
                IsInformationTimeSensitive = p => p is NbaGameScores,
                IsInformationIncentiveLost = lp => false,
                //court finds no competition between plaintiff and defendant
                IsInformationUseDirectCompetition = lp => false,
            };
            var testResult = test.IsValid(new NationalBasketballAssociation(), new MotorolaInc());
            Assert.False(testResult);
            this.output.WriteLine(test.ToString());
        }
    }

    public class NbaGameScores : IntangiblePersonalProperty
    {

    }

    public class NationalBasketballAssociation : LegalPerson, IPlaintiff
    {
        public NationalBasketballAssociation(): base("National Basketball Association") { }
    }

    public class MotorolaInc : LegalPerson, ITortfeasor
    {
        public MotorolaInc(): base("Motorola, Inc.") { }
    }
}
