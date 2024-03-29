﻿using System;
using NoFuture.Law.Tort.US.IntentionalTort;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Tort.Tests
{
    public class ExampleUnjustEnrichmentTests
    {
        private readonly ITestOutputHelper output;

        public ExampleUnjustEnrichmentTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestUnjustEnrichmentIsValid()
        {
            var test = new UnjustEnrichment(ExtensionMethods.Tortfeasor)
            {
                IsEnriched = lp => lp is SomeDefendant,
                IsImpoverished = lp => lp is SomePlaintiff,
                IsOtherwiseWithoutRemedyAtLaw = lp => lp is SomePlaintiff,
                LegalCause = new ProximateCause(ExtensionMethods.Tortfeasor)
                {
                    IsForeseeable = lp => lp is SomeDefendant,
                    IsDirectCause = lp => lp is SomeDefendant
                }
            };

            var testResult = test.IsValid(new SomePlaintiff(), new SomeDefendant());
            Assert.True(testResult);
            this.output.WriteLine(test.ToString());
        }
    }

    public class SomePlaintiff : LegalPerson, IPlaintiff
    {
        public SomePlaintiff() : base("Some P. Laintiff") { }
    }

    public class SomeDefendant : LegalPerson, ITortfeasor
    {
        public SomeDefendant() : base("Billy B Defendant") { }
    }
}
