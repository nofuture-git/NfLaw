﻿using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Defense;
using NoFuture.Law.Criminal.US.Defense.Justification;
using NoFuture.Law.Criminal.US.Elements;
using NoFuture.Law.Criminal.US.Elements.Act;
using NoFuture.Law.Criminal.US.Elements.Intent.PenalCode;
using NoFuture.Law.Criminal.US.Terms;
using NoFuture.Law.Criminal.US.Terms.Violence;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using NUnit.Framework;

namespace NoFuture.Law.Criminal.Tests.DefenseTests.DefenseOfSelfTests
{
    [TestFixture]
    public class ExampleAppropriateDeadlyForceTests
    {
        [Test]
        public void ExampleAppropriateDeadlyForce()
        {
            var testCrime = new Felony
            {
                ActusReus = new ActusReus
                {
                    IsVoluntary = lp => lp is WandaEg,
                    IsAction = lp => lp is WandaEg
                },
                MensRea = new Knowingly
                {
                    IsKnowledgeOfWrongdoing = lp => lp is WandaEg,
                    IsIntentOnWrongdoing = lp => lp is WandaEg
                }
            };

            var testResult = testCrime.IsValid(new WandaEg());
            Assert.IsTrue(testResult);

            var testSubject = new DefenseOfSelf
            {
                IsReasonableFearOfInjuryOrDeath = lp => true,
                Imminence = new Imminence(ExtensionMethods.Defendant)
                {
                    GetResponseTime = lp => lp is WandaEg ? Imminence.NormalReactionTimeToDanger : TimeSpan.Zero
                },
                Provocation = new Provocation(ExtensionMethods.Defendant)
                {
                    IsInitiatorOfAttack = lp => lp is NicholasEg
                },
                Proportionality = new Proportionality<ITermCategory>(ExtensionMethods.Defendant)
                {
                    //deadly force is appropriate to serious bodily harm
                    GetChoice = lp =>
                    {
                        if (lp is WandaEg)
                            return new DeadlyForce();
                        if (lp is NicholasEg)
                            return new SeriousBodilyInjury();
                        return null;
                    }
                }
            };
            testResult = testSubject.IsValid(new WandaEg(), new NicholasEg());
            Console.WriteLine(testSubject.ToString());
            Assert.IsTrue(testResult);
        }
    }

    public class NicholasEg : LegalPerson
    {
        public NicholasEg() :base("NICHOLAS") {}
    }

    public class WandaEg : LegalPerson, IDefendant
    {
        public WandaEg() : base("WANDA") {}
    }
}
