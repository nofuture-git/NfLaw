﻿using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Defense.Excuse.Insanity;
using NoFuture.Law.Criminal.US.Elements.Act;
using NoFuture.Law.Criminal.US.Elements.Intent.ComLaw;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using NUnit.Framework;

namespace NoFuture.Law.Criminal.Tests.DefenseTests.InsanityTests
{
    /// <summary>
    /// State v. Hornsby, 484 S.E.2d 869 (1997)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, guilty-but-mentally-ill is a half-baked insanity where the only part that is 
    /// true is the mental defect part
    /// ]]>
    /// </remarks>
    [TestFixture()]
    public class StatevHornsbyTests
    {
        [Test]
        public void StatevHornsby()
        {
            var testCrime = new Felony
            {
                ActusReus = new ActusReus
                {
                    IsAction = lp => lp is Hornsby,
                    IsVoluntary = lp => lp is Hornsby,
                },
                MensRea = new GeneralIntent
                {
                    IsKnowledgeOfWrongdoing = lp => lp is Hornsby,
                    IsIntentOnWrongdoing = lp => lp is Hornsby,
                }
            };

            var testResult = testCrime.IsValid(new Hornsby());
            Assert.IsTrue(testResult);

            var testSubject = new MNaghten
            {
                IsMentalDefect = lp => lp is Hornsby,
            };
            testResult = testSubject.IsValid(new Hornsby());
            Console.WriteLine(testSubject.ToString());
            Assert.IsFalse(testResult);
        }
    }

    public class Hornsby : LegalPerson, IDefendant
    {
        public Hornsby() : base("BRENT HORNSBY") { }
    }
}
