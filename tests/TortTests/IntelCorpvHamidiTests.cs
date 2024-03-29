﻿using System;
using NoFuture.Law.Property.US.FormsOf;
using Xunit;
using NoFuture.Law.US.Persons;
using NoFuture.Law.Tort.US.IntentionalTort;
using NoFuture.Law.US;
using Xunit.Abstractions;

namespace NoFuture.Law.Tort.Tests
{
    /// <summary>
    /// Intel Corp. v. Hamidi, 71 P.3d 296 (Cal. 2003)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, its not trespass to personal property without damage or loss of possession
    /// ]]>
    /// </remarks>
    public class IntelCorpvHamidiTests
    {
        private readonly ITestOutputHelper output;

        public IntelCorpvHamidiTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void IntelCorpvHamidi()
        {
            var testSubject = new TrespassToChattels
            {
                Consent = new Consent
                {
                    IsApprovalExpressed = lp => false,
                    IsCapableThereof = lp => lp is IntelCorp
                },
                SubjectProperty = new PersonalProperty
                {
                    Name = "Intel Corp computer system",
                    IsEntitledTo = lp => lp is IntelCorp,
                    IsInPossessionOf = lp => lp is IntelCorp
                },
                Causation =  new Causation(ExtensionMethods.Tortfeasor)
                {
                    FactualCause = new FactualCause(ExtensionMethods.Tortfeasor)
                    {
                        IsButForCaused = lp => lp is Hamidi
                    },
                    ProximateCause = new ProximateCause(ExtensionMethods.Tortfeasor)
                    {
                        IsForeseeable = lp => lp is Hamidi
                    }
                },
                Injury = new Damage(ExtensionMethods.Tortfeasor)
                {
                    ToNormalFunction = p => false,
                    ToUsefulness =  p => false,
                    ToValue = p => false
                },
                IsCauseDispossession = lp => false
            };

            var testResult = testSubject.IsValid(new IntelCorp(), new Hamidi());
            this.output.WriteLine(testSubject.ToString());
            Assert.False(testResult);
        }
    }

    public class IntelCorp : LegalPerson, IPlaintiff
    {

    }

    public class Hamidi : LegalPerson, ITortfeasor
    {

    }
}
