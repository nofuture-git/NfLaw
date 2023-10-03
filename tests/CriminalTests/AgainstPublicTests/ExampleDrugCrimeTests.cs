﻿using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.AgainstPublic;
using NoFuture.Law.Criminal.US.Elements.Intent.ComLaw;
using NoFuture.Law.Criminal.US.Elements.Intent.PenalCode;
using NoFuture.Law.Criminal.US.Terms;
using NoFuture.Law.US.Persons;
using NUnit.Framework;

namespace NoFuture.Law.Criminal.Tests.AgainstPublicTests
{
    [TestFixture]
    public class ExampleDrugCrimeTests
    {
        [Test]
        public void TestDrugManfacture()
        {
            var testCrime = new Felony
            {
                ActusReus = new DrugManufacture
                {
                    IsManufacturer = lp => true,
                    Offer = new ScheduleI("bath salts")
                },
                MensRea = new Purposely
                {
                    IsKnowledgeOfWrongdoing = lp => true,
                    IsIntentOnWrongdoing = lp => true,
                }
            };
            var testResult = testCrime.IsValid(new CriminalNameHereEg());
            Console.WriteLine(testCrime.ToString());
            Assert.IsTrue(testResult);
        }

        [Test]
        public void TestDrugPossession()
        {
            var testCrime = new Misdemeanor
            {
                ActusReus = new DrugPossession
                {
                    IsKnowinglyProcured = lp => true,
                    Offer = new ScheduleI("pot")
                },
                MensRea = new GeneralIntent
                {
                    IsKnowledgeOfWrongdoing = lp => true,
                }
            };
            var testResult = testCrime.IsValid(new CriminalNameHereEg());
            Console.WriteLine(testCrime.ToString());
            Assert.IsTrue(testResult);
        }

        [Test]
        public void TestDrugSale()
        {
            var testCrime = new Misdemeanor
            {
                ActusReus = new DrugSale
                {
                    Acceptance = drug => drug is ScheduleI ? new LegalProperty("money"){PropertyValue = dt => 150m} : null,
                    Assent = new Deal
                    {
                        IsApprovalExpressed = lp => true
                    },
                    Offer = new ScheduleI("pot")
                },
                MensRea = new Purposely
                {
                    IsKnowledgeOfWrongdoing = lp => true,
                }
            };
            var testResult = testCrime.IsValid(new CriminalNameHereEg());
            Console.WriteLine(testCrime.ToString());
            Assert.IsTrue(testResult);
        }

        [Test]
        public void TestDrugUse()
        {
            var testCrime = new Misdemeanor
            {
                ActusReus = new DrugUse
                {
                    IsUnderInfluence = lp => true,
                    Offer = new ScheduleI("pot")
                },
                MensRea = new Purposely
                {
                    IsKnowledgeOfWrongdoing = lp => true,
                }
            };
            var testResult = testCrime.IsValid(new CriminalNameHereEg());
            Console.WriteLine(testCrime.ToString());
            Assert.IsTrue(testResult);
        }
    }

    public class CriminalNameHereEg : LegalPerson, IDefendant
    {
        public CriminalNameHereEg(): base("CRIMINAL NAME HERE") {  }
    }
}
