﻿using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Defense.Excuse;
using NoFuture.Law.Criminal.US.Elements.Act;
using NoFuture.Law.Criminal.US.Elements.Inchoate;
using NoFuture.Law.Criminal.US.Elements.Intent.ComLaw;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.InchoateTests
{
    public class ExampleAbandonmentTests
    {
        private readonly ITestOutputHelper output;

        public ExampleAbandonmentTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void VoluntaryAbandonmentTest()
        {
            var testCrime = new Misdemeanor
            {
                MensRea = new GeneralIntent
                {
                    IsKnowledgeOfWrongdoing = lp => lp is MelissaEg3,
                    IsIntentOnWrongdoing = lp => lp is MelissaEg3
                },
                ActusReus = new Attempt
                {
                    SubstantialSteps = new SubstantialSteps
                    {
                        IsPossessCriminalMaterial = lp => lp is MelissaEg3
                    }
                }
            };

            var testResult = testCrime.IsValid(new MelissaEg3());
            Assert.True(testResult);

            var testSubject = new Abandonment(testCrime)
            {
                IsMotivatedByFearOfGettingCaught = lp => !MelissaEg3.IsAbandon(lp),
                IsMotivatedByNewDifficulty = lp => !MelissaEg3.IsAbandon(lp)
            };

            testResult = testSubject.IsValid(new MelissaEg3());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
        }
    }

    public class MelissaEg3 : MelissaEg2
    {
        public bool IsAwwedOverByPuppy { get; set; } = true;

        public static bool IsAbandon(ILegalPerson lp)
        {
            var melissa = lp as MelissaEg3;
            if (melissa == null)
                return false;
            return melissa.IsAwwedOverByPuppy;
        }
    }
}
