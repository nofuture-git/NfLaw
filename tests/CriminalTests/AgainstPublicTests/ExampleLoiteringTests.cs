﻿using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.AgainstPublic;
using NoFuture.Law.Criminal.US.Elements.Intent.PenalCode;
using NoFuture.Law.US.Persons;
using Xunit;

namespace NoFuture.Law.Criminal.Tests.AgainstPublicTests
{
    
    public class ExampleLoiteringTests
    {
        [Fact]
        public void TestLoiteringIsValid()
        {
            var testCrime = new Misdemeanor
            {
                ActusReus = new Loitering
                {
                    IsBegging = lp => lp is SomeBumEg,
                },
                MensRea = new Purposely
                {
                    IsIntentOnWrongdoing = lp => lp is SomeBumEg
                }
            };

            var testResult = testCrime.IsValid(new SomeBumEg());
            Console.WriteLine(testCrime.ToString());
            Assert.IsTrue(testResult);
        }
    }

    public class SomeBumEg : LegalPerson, IDefendant
    {
        public SomeBumEg() : base("ARRRAGHH! GRINPNKLOOP...") { }
    }
}
