﻿using System;
using NUnit.Framework;
using NoFuture.Law.US.Persons;
using NoFuture.Law.Tort.US.Elements;
using NoFuture.Law.Tort.US.IntentionalTort;
using NoFuture.Law.US;

namespace NoFuture.Law.Tort.Tests
{
    /// <summary>
    /// Mohr v. Williams, 104 N.W. 12, 13-16 (Minn. 1905)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, consent may depend on specific directions and not the general intent
    /// ]]>
    /// </remarks>
    [TestFixture]
    public class MohrvWilliamsTests
    {
        [Test]
        public void MohrvWilliams()
        {
            var test = new TrespassToChattels
            {
                Causation = new Causation(ExtensionMethods.Tortfeasor)
                {
                    FactualCause = new FactualCause(ExtensionMethods.Tortfeasor)
                    {
                        IsButForCaused = lp => lp is Mohr
                    },
                    ProximateCause = new ProximateCause(ExtensionMethods.Tortfeasor)
                    {
                        IsForeseeable = lp => true
                    }
                },
                //the consent was for the other ear 
                Consent = new Consent(ExtensionMethods.Tortfeasor)
                {
                    IsApprovalExpressed = lp => false,
                    IsCapableThereof = lp => true
                },
                IsCauseDispossession = lp => true,
                IsTangibleEntry = lp => true,
                Injury = new Damage(ExtensionMethods.Tortfeasor)
                {
                    SubjectProperty = new LegalProperty("Williams hearing")
                    {
                        IsEntitledTo = lp => lp is Williams,
                        IsInPossessionOf = lp => lp is Williams,
                    },
                    ToNormalFunction = lp => true
                },
                SubjectProperty = new LegalProperty("Williams hearing")
            };

            var testResult = test.IsValid(new Mohr(), new Williams());
            Console.WriteLine(test.ToString());
            Assert.IsTrue(testResult);
        }
    }

    public class Mohr : LegalPerson, ITortfeasor
    {
        public Mohr(): base("") { }
    }

    public class Williams : LegalPerson, IPlaintiff, IVictim
    {
        public Williams(): base("") { }
    }
}
