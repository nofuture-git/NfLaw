﻿using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.Act;
using NoFuture.Law.Criminal.US.Elements.Homicide;
using NoFuture.Law.Criminal.US.Elements.Intent.PenalCode;
using NoFuture.Law.US.Persons;
using NUnit.Framework;

namespace NoFuture.Law.Criminal.Tests.HomicideTests
{
    [TestFixture()]
    public class ExampleHomicideInOtherTests
    {
        [Test]
        public void ExampleFelonyMurder()
        {
            var testFirstCrime = new Felony
            {
                ActusReus = new ActusReus
                {
                    IsVoluntary = lp => lp is JouquinBurnerEg,
                    IsAction = lp => lp is JouquinBurnerEg
                },
                MensRea = new Recklessly
                {
                    IsUnjustifiableRisk = lp => lp is JouquinBurnerEg,
                    IsDisregardOfRisk = lp => lp is JouquinBurnerEg
                }
            };

            var testResult = testFirstCrime.IsValid(new JouquinBurnerEg());
            Console.WriteLine(testFirstCrime.ToString());
            Assert.IsTrue(testResult);

            var testCrime = new Felony
            {
                ActusReus = new HomicideInOther(testFirstCrime)
                {
                    IsCorpusDelicti = lp => lp is JouquinBurnerEg
                },
                MensRea = testFirstCrime.MensRea
            };

            testResult = testCrime.IsValid(new JouquinBurnerEg());
            Console.WriteLine(testCrime.ToString());
            Assert.IsTrue(testResult);
        }

        [Test]
        public void ExampleDeathOccursBefore()
        {
            var testFirstCrime = new Felony
            {
                ActusReus = new ActusReus
                {
                    IsVoluntary = lp => lp is JouquinBurnerEg,
                    IsAction = lp => lp is JouquinBurnerEg
                },
                MensRea = new Recklessly
                {
                    IsUnjustifiableRisk = lp => lp is JouquinBurnerEg,
                    IsDisregardOfRisk = lp => lp is JouquinBurnerEg
                }
            };

            var testResult = testFirstCrime.IsValid(new JouquinBurnerEg());
            Console.WriteLine(testFirstCrime.ToString());
            Assert.IsTrue(testResult);
            var yyyy = DateTime.Today.Year;

            var testCrime = new Felony
            {
                ActusReus = new HomicideInOther(testFirstCrime)
                {
                    IsCorpusDelicti = lp => lp is JouquinBurnerEg,
                    Inception = new DateTime(yyyy, 3, 15, 9, 0, 0),
                    Terminus = new DateTime(yyyy, 3, 15, 9, 45, 0),
                    TimeOfTheDeath = new DateTime(yyyy, 3, 15, 10, 0, 0),
                },
                MensRea = testFirstCrime.MensRea
            };

            testResult = testCrime.IsValid(new JouquinBurnerEg());
            Console.WriteLine(testCrime.ToString());
            Assert.IsFalse(testResult);
        }

        [Test]
        public void ExampeMidemeanorManslaughter()
        {
            var testInputCrime = new Misdemeanor
            {
                ActusReus = new ActusReus
                {
                    IsVoluntary = lp => lp is RobertaBrandishEg,
                    IsAction = lp => lp is RobertaBrandishEg
                },
                MensRea = new Negligently
                {
                    IsUnjustifiableRisk = lp => lp is RobertaBrandishEg,
                    IsUnawareOfRisk = lp => lp is RobertaBrandishEg
                }
            };

            var testResult = testInputCrime.IsValid(new RobertaBrandishEg());
            Console.WriteLine(testInputCrime);
            Assert.IsTrue(testResult);

            var testCrime = new Felony
            {
                ActusReus = new HomicideInOther(testInputCrime)
                {
                    IsCorpusDelicti = lp => lp is RobertaBrandishEg
                },
                MensRea = testInputCrime.MensRea
            };

            testResult = testCrime.IsValid(new RobertaBrandishEg());
            Console.WriteLine(testCrime.ToString());
            Assert.IsTrue(testResult);
        }
    }

    public class JouquinBurnerEg : LegalPerson, IDefendant
    {
        public JouquinBurnerEg() : base("JOUQUIN BURNER") { }
    }

    public class RobertaBrandishEg : LegalPerson, IDefendant
    {
        public RobertaBrandishEg(): base("ROBERTA BRANDISH") { }
    }
}
