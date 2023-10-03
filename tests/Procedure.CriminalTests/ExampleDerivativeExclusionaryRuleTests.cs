﻿using System;
using NoFuture.Law;
using NoFuture.Law.Procedure.Criminal.US.Challenges;
using NUnit.Framework;

namespace NoFuture.Law.Procedure.Criminal.Tests
{
    [TestFixture]
    public class ExampleDerivativeExclusionaryRuleTests
    {
        [Test]
        public void TestDerivativeExclusionaryRuleIsValid00()
        {
            var testSubject = new DerivativeExclusionaryRule<IVoca>
            {
                IsDerivedFromUnlawfulSource = v => true,
                IsInterveningEventsAttenuated = v => v is ExampleContraband,
                GetDerivativeEvidence = lp => lp is ExampleSuspect ? new ExampleContraband() : null,
            };

            var testResult = testSubject.IsValid(new ExampleSuspect(), new ExampleLawEnforcement());
            Console.WriteLine(testSubject.ToString());
            Assert.IsFalse(testResult);

        }
    }
}
