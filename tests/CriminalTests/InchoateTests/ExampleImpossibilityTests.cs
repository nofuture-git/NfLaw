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
    public class ExampleImpossibilityTests
    {
        private readonly ITestOutputHelper output;

        public ExampleImpossibilityTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        //Impossibility is only applicable to Attempt
        public void TestOnlyForAttempt()
        {
            var testCrime = new Misdemeanor
            {
                MensRea = new GeneralIntent
                {
                    IsKnowledgeOfWrongdoing = lp => lp is MelissaEg2,
                    IsIntentOnWrongdoing = lp => lp is MelissaEg2
                },
                ActusReus = new ActusReus
                {
                    IsVoluntary = lp => lp is MelissaEg2,
                    IsAction = lp => lp is MelissaEg2
                }
            };

            var testResult = testCrime.IsValid(new MelissaEg2());
            Assert.True(testResult);

            var testSubject = new Impossibility(testCrime)
            {
                IsLegalImpossibility = MelissaEg2.IsLegal
            };

            var melissa = new MelissaEg2 { ItActuallyIsRatThatsBarking = true };
            testResult = testSubject.IsValid(melissa);
            this.output.WriteLine(testSubject.ToString());
            Assert.False(testResult);
        }

        [Fact]
        /*
        Melissa trys to poison neighbors dog for 
        barking but it was actually a giant rat that 
        was barking. Killing the rat is not illegal.
         */
        public void LegalImpossibilityTest()
        {

            var testCrime = new Misdemeanor
            {
                MensRea = new GeneralIntent
                {
                    IsKnowledgeOfWrongdoing = lp => lp is MelissaEg2,
                    IsIntentOnWrongdoing = lp => lp is MelissaEg2
                },
                ActusReus = new Attempt
                {
                    SubstantialSteps = new SubstantialSteps
                    {
                        IsPossessCriminalMaterial = lp => lp is MelissaEg2,
                    }
                }
            };

            var testResult = testCrime.IsValid(new MelissaEg2());
            Assert.True(testResult);

            var testSubject = new Impossibility(testCrime)
            {
                IsLegalImpossibility = MelissaEg2.IsLegal
            };

            var melissa = new MelissaEg2 {ItActuallyIsRatThatsBarking = true};
            testResult = testSubject.IsValid(melissa);
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
        }

        [Fact]
        /*
         Melissa trys to poison neighbors dog 
         for barking but its not in the yard at the time.
         */
        public void FactualImpossibilityTest()
        {
            var testCrime = new Misdemeanor
            {
                MensRea = new GeneralIntent
                {
                    IsKnowledgeOfWrongdoing = lp => lp is MelissaEg2,
                    IsIntentOnWrongdoing = lp => lp is MelissaEg2
                },
                ActusReus = new Attempt
                {
                    SubstantialSteps = new SubstantialSteps
                    {
                        IsInvestigatingPotentialScene = lp => lp is MelissaEg2,
                        IsPossessCriminalMaterial = lp => lp is MelissaEg2
                    }
                }
            };

            var testResult = testCrime.IsValid(new MelissaEg2());
            Assert.True(testResult);

            var testSubject = new Impossibility(testCrime)
            {
                IsFactualImpossibility = MelissaEg2.IsFactual
            };
            
            testResult = testSubject.IsValid(new MelissaEg2());
            this.output.WriteLine(testSubject.ToString());
            Assert.False(testResult);
        }
    }

    public class MelissaEg2 : MelissaEg
    {
        public bool ItActuallyIsRatThatsBarking { get; set; }
        public bool IsRatAPest { get; set; } = true;
        public bool IsDogPresent { get; set; }

        public static bool IsLegal(ILegalPerson lp)
        {
            var melissa = lp as MelissaEg2;
            if (melissa == null)
                return false;
            return melissa.ItActuallyIsRatThatsBarking && melissa.IsRatAPest;
        }

        public static bool IsFactual(ILegalPerson lp)
        {
            var melissa = lp as MelissaEg2;
            if (melissa == null)
                return false;
            return !melissa.IsDogPresent;
        }
    }
}
