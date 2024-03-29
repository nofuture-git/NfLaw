﻿using System;
using NoFuture.Law.Tort.US.Elements;
using NoFuture.Law.US;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Tort.Tests
{
    public class ExampleVicariousLiabilityTests
    {
        private readonly ITestOutputHelper output;

        public ExampleVicariousLiabilityTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestVicariousLiabilityIsValid()
        {
            var test = new VicariousLiability(ExtensionMethods.Defendant)
            {
                IsAction = lp => lp is SomeDefendant,
                IsVoluntary = lp => lp is SomeDefendant,
                IsMutuallyBeneficialRelationship = (l1, l2) =>
                    l1 is Some3rdParty && l2 is SomeDefendant || l1 is SomeDefendant && l2 is Some3rdParty,
                IsAttempt2LimitOthersAct = lp => false
            };
            var testResult = test.IsValid(new SomePlaintiff(), new SomeDefendant(), new Some3rdParty());
            Assert.True(testResult);
            this.output.WriteLine(test.ToString());
        }
    }
}
