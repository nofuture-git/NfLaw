﻿using System;
using NoFuture.Law.Property.US.Acquisition;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Property.Tests
{
    /// <summary>
    /// Brown v. Gobble, 474 S.E.2d 489 (W. Va. 1996)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, example of adverse possession with the concept of "tacking" (where prior owners already satisfied adverse possession)
    /// ]]>
    /// </remarks>
    public class BrownvGobbleTests
    {
        private readonly ITestOutputHelper output;

        public BrownvGobbleTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void BrownvGobble()
        {
            var test = new AdversePossession(ExtensionMethods.Disseisor)
            {
                SubjectProperty = new TwoFeetWideTract
                {
                    IsEntitledTo = lp => lp is Gobble,
                    IsInPossessionOf = lp => lp is Brown,
                },
                Consent = Consent.NotGiven(),
                IsOpenNotoriousPossession = p => p is Brown,
                IsExclusivePossession = p => p is Brown,
                IsContinuousPossession = p => p is Brown,
                Inception = new DateTime(1931,1,1)
            };

            var testResult = test.IsValid(new Brown(), new Gobble());
            this.output.WriteLine(test.ToString());
            Assert.True(testResult);
        }
    }

    public class TwoFeetWideTract : LegalProperty
    {
        public TwoFeetWideTract(): base("two-feet-wide tract") { }
    }

    public class Brown : LegalPerson, IPlaintiff, IDisseisor
    {
        public Brown(): base("Brown") { }
    }

    public class Gobble : LegalPerson, IDefendant
    {
        public Gobble(): base("Gobble") { }
    }
}
