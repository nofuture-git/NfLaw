﻿using System;
using NoFuture.Law.Tort.US.Elements;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using NUnit.Framework;

namespace NoFuture.Law.Tort.Tests
{
    /// <summary>
    /// Golden Press, Inc. v. Rylands, 235 P.2d 592 (Colo. 1951).
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, encroachment can be ignored is some slight circumstances
    /// ]]>
    /// </remarks>
    [TestFixture]
    public class GoldenPressIncvRylandsTests
    {
        [Test]
        public void GoldenPressIncvRylands()
        {
            var test = new Encroachment
            {
                Consent = Consent.NotGiven(),
                SubjectProperty = new LegalProperty("some building")
                {
                    IsEntitledTo = lp => lp is Rylands
                },
                IsGoodFaithIntent = lp => true,
                IsUseAffected = lp => false,
                IsRemovalCostGreat = lp => lp is Rylands
            };
            var testResult = test.IsValid(new GoldenPressInc(), new Rylands());
            Assert.IsFalse(testResult);
            Console.WriteLine(test.ToString());
        }
    }

    public class GoldenPressInc : LegalPerson, IPlaintiff
    {
        public GoldenPressInc(): base("Golden Press, Inc.") { }
    }

    public class Rylands : LegalPerson, IDefendant
    {
        public Rylands(): base("Rylands") { }
    }
}
