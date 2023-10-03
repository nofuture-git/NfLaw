﻿using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.AgainstPersons;
using NoFuture.Law.Criminal.US.Elements.Intent.ComLaw;
using NoFuture.Law.US.Persons;
using NUnit.Framework;

namespace NoFuture.Law.Criminal.Tests.HominiLupusTests
{
    [TestFixture]
    public class ExampleIncestTests
    {
        [Test]
        public void ExampleIncest()
        {
            var testCrime = new Felony
            {
                ActusReus = new Incest
                {
                    IsSexualIntercourse = lp => lp is HalIncestEg || lp is HarrietIncestEg,
                    IsOneOfTwo = lp => lp is HalIncestEg || lp is HarrietIncestEg,
                    IsFamilyRelation = (lp1, lp2) => (lp1 is HalIncestEg || lp1 is HarrietIncestEg)
                                                     && (lp2 is HalIncestEg || lp2 is HarrietIncestEg)
                },
                MensRea = new GeneralIntent
                {
                    IsKnowledgeOfWrongdoing = lp => lp is HalIncestEg || lp is HarrietIncestEg
                }
            };

            var testResult = testCrime.IsValid(new HalIncestEg(), new HarrietIncestEg());
            Console.WriteLine(testCrime.ToString());
            Assert.IsTrue(testResult);
        }

        [Test]
        public void TestIntentIsNull()
        {
            var testCrime = new Felony
            {
                ActusReus = new Incest
                {
                    IsSexualIntercourse = lp => lp is HalIncestEg || lp is HarrietIncestEg,
                    IsOneOfTwo = lp => lp is HalIncestEg || lp is HarrietIncestEg,
                    IsFamilyRelation = (lp1, lp2) => (lp1 is HalIncestEg || lp1 is HarrietIncestEg)
                                                     && (lp2 is HalIncestEg || lp2 is HarrietIncestEg)
                },
                MensRea = null
            };

            var testResult = testCrime.IsValid(new HalIncestEg(), new HarrietIncestEg());
            Console.WriteLine(testCrime.ToString());
            Assert.IsFalse(testResult);
        }
    }

    public class HalIncestEg : LegalPerson, IDefendant
    {
        public HalIncestEg() : base("HAL INCEST") { }
    }

    public class HarrietIncestEg : LegalPerson, IDefendant
    {
        public HarrietIncestEg() : base("HARRIET INCEST") { }
    }
}
