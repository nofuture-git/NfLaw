﻿using System;
using NoFuture.Law.Property.US.FormsOf.InTerra;
using NoFuture.Law.Property.US.FormsOf.InTerra.Sequential;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Property.Tests
{
    /// <summary>
    /// Berg v. Wiley, 264 N.W.2d 145 (Minn. 1978).
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, eviction requires peaceful repossession 
    /// ]]>
    /// </remarks>
    public class BergvWileyTests
    {
        private readonly ITestOutputHelper output;

        public BergvWileyTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void BergvWiley()
        {
            var testLease = new Leasehold()
            {
                Inception = new DateTime(1970, 12, 1),
                SubjectProperty = new RealProperty("building in Osseo, Minnesota")
                    {IsInPossessionOf = lp => lp is Berg, IsEntitledTo = lp => lp is Wiley},
                Terminus = new DateTime(1975, 12, 1)
            };

            var testResult = testLease.IsValid(new Berg(), new Wiley());
            Assert.True(testResult);

            var test = new Eviction(testLease)
            {
                CurrentDateTime = new DateTime(1973, 7, 16),
                IsBreachLeaseCondition = lp => false,
                //court concludes changing locks in secret is not peaceable
                IsPeaceableSelfHelpReentry = lp => !(lp is Wiley)
            };

            testResult = test.IsValid(new Berg(), new Wiley());
            this.output.WriteLine(test.ToString());
            Assert.False(testResult);
        }
    }

    public class Berg : LegalPerson, IPlaintiff, ILessee
    {
        public Berg(): base("Berg") { }
    }

    public class Wiley : LegalPerson, IDefendant, ILessor
    {
        public Wiley(): base("Wiley") { }
    }
}
