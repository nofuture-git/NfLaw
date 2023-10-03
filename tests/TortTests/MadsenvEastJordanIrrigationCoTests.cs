﻿using System;
using Xunit;
using NoFuture.Law.US.Persons;
using NoFuture.Law.Tort.US.IntentionalTort;
using NoFuture.Law.US;
using Xunit.Abstractions;

namespace NoFuture.Law.Tort.Tests
{
    /// <summary>
    /// Madsen v. East Jordan Irrigation Co., 125 P.2d 794 (Utah 1942)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, when the loose requirements of cause in abnormal danger acts is just too loose
    /// ]]>
    /// </remarks>
    public class MadsenvEastJordanIrrigationCoTests
    {
        private readonly ITestOutputHelper output;

        public MadsenvEastJordanIrrigationCoTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void MadsenvEastJordanIrrigationCo()
        {
            var property = new LegalProperty("cannibal minks");

            var test = new AbnormallyDangerousActivity(ExtensionMethods.Tortfeasor)
            {
                Causation = new Causation(ExtensionMethods.Tortfeasor)
                {
                    FactualCause = new FactualCause(ExtensionMethods.Tortfeasor)
                    {
                        IsButForCaused = lp => lp is EastJordanIrrigationCo
                    },
                    ProximateCause = new ProximateCause(ExtensionMethods.Tortfeasor)
                    {
                        IsDirectCause = lp => false,
                        IsForeseeable = lp => false,
                    }
                },
                IsExplosives = p => true,
                SubjectProperty = property,
                Injury = new Damage(ExtensionMethods.Tortfeasor)
                {
                    SubjectProperty = property,
                    ToValue = p => true //they are dead
                }
            };

            var testResult = test.IsValid(new Madsen(), new EastJordanIrrigationCo());
            this.output.WriteLine(test.ToString());
            Assert.False(testResult);
        }
    }

    public class Madsen : LegalPerson, IPlaintiff
    {
        public Madsen(): base("Madsen") { }
    }

    public class EastJordanIrrigationCo : LegalPerson, ITortfeasor
    {
        public EastJordanIrrigationCo(): base("East Jordan Irrigation Co.") { }
    }
}
