﻿using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Defense.Excuse.Insanity;
using NoFuture.Law.Criminal.US.Elements.Act;
using NoFuture.Law.Criminal.US.Elements.Intent.ComLaw;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.DefenseTests.InsanityTests
{
    public class ExampleSubstanCapacityTests
    {
        private readonly ITestOutputHelper output;

        public ExampleSubstanCapacityTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ExampleSubstantialCapacity()
        {
            var testCrime = new Felony
            {
                ActusReus = new ActusReus
                {
                    IsVoluntary = lp => lp is LoreenEg,
                    IsAction = lp => lp is LoreenEg,
                },
                MensRea = new GeneralIntent
                {
                    IsKnowledgeOfWrongdoing = lp => lp is LoreenEg,
                }
            };

            var testResult = testCrime.IsValid(new LoreenEg());
            Assert.True(testResult);

            var testSubject = new SubstantialCapacity
            {
                IsMentalDefect = lp => lp is LoreenEg,
                IsMostlyWrongnessOfAware = lp => !(lp is LoreenEg),
                IsMostlyVolitional = lp => !(lp is LoreenEg)
            };

            testResult = testSubject.IsValid(new LoreenEg());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
        }
    }

    public class LoreenEg : LegalPerson, IDefendant
    {
        public LoreenEg() : base("LOREEN MADHOUSE") { }
    }
}
