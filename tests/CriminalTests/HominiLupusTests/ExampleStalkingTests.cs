﻿using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.AgainstPersons;
using NoFuture.Law.Criminal.US.Elements.AgainstPersons.Credible;
using NoFuture.Law.Criminal.US.Elements.Intent.ComLaw;
using NoFuture.Law.US.Persons;
using NUnit.Framework;

namespace NoFuture.Law.Criminal.Tests.HominiLupusTests
{
    [TestFixture]
    public class ExampleStalkingTests
    {
        [Test]
        public void ExampleNotStalking()
        {
            var testCrime = new Misdemeanor
            {
                ActusReus = new Stalking
                {
                    Occasions = new[] {new DeclareLove(), new DeclareLove(),},
                    IsApparentAbility = lp => lp is ElliotStalkerEg
                },
                MensRea = new GeneralIntent
                {
                    IsKnowledgeOfWrongdoing = lp => lp is ElliotStalkerEg
                }
            };
            var testResult = testCrime.IsValid(new ElliotStalkerEg());
            Console.WriteLine(testCrime.ToString());
            Assert.IsFalse(testResult);
        }

        [Test]
        public void ExampleIsStalking()
        {
            var testCrime = new Misdemeanor
            {
                ActusReus = new Stalking
                {
                    Occasions = new IAgitate[]
                    {
                        new Pursue
                        {
                            IsCauseToFearSafety = lp => lp is ElliotStalkerEg
                        },
                        new Approach
                        {
                            IsCauseToFearSafety = lp => lp is ElliotStalkerEg
                        },
                        new ThreateningWords("make her pay")
                        {
                            IsCauseToFearSafety = lp => lp is ElliotStalkerEg
                        },
                        new ThreateningGesture("carving death threat on door")
                        {
                            IsSubstantialEmotionalDistress = lp => lp is ElliotStalkerEg
                        }, 
                    },
                    IsApparentAbility = lp => lp is ElliotStalkerEg
                },
                MensRea = new GeneralIntent
                {
                    IsKnowledgeOfWrongdoing = lp => lp is ElliotStalkerEg
                }
            };
            var testResult = testCrime.IsValid(new ElliotStalkerEg());
            Console.WriteLine(testCrime.ToString());
            Assert.IsTrue(testResult);
        }
    }

    public class DeclareLove : CredibleBase
    {
        public override bool IsValid(params ILegalPerson[] persons)
        {
            AddReasonEntry("declaring love isn't illegal nor substantial - just annoying");
            return false;
        }
    }

    public class ElliotStalkerEg : LegalPerson, IDefendant
    {
        public ElliotStalkerEg() : base("ELLIOT STALKER") { }
    }
}
