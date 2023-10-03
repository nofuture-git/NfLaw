﻿using System;
using NoFuture.Law.Criminal.US.Elements.AgainstProperty.Theft;
using NoFuture.Law.US.Persons;
using NUnit.Framework;

namespace NoFuture.Law.Criminal.Tests.TheftTests
{
    [TestFixture]
    public class ExampleByReceivingTests
    {
        [Test]
        public void ExampleByReceivingActTest()
        {
            var testAct = new ByReceiving
            {
                IsPresentStolen = lp => lp is ChanelFenceEg || lp is BurtThiefEg,
                IsTakenPossession = lp => lp is ChanelFenceEg || lp is BurtThiefEg || lp is SandraVictimEg,
                SubjectProperty = new LegalProperty("designer perfume"){ PropertyValue = dt => 5000m },
            };

            var testResult = testAct.IsValid(new ChanelFenceEg(), new BurtThiefEg(), new SandraVictimEg());
            Console.WriteLine(testAct.ToString());
            Assert.IsTrue(testResult);
        }
    }

    public class ChanelFenceEg : LegalPerson, IDefendant
    {
        public ChanelFenceEg(): base("CHANEL FENCE") {  }
    }

    public class BurtThiefEg : LegalPerson, IDefendant
    {
        public BurtThiefEg() : base("BURT THIEF") { }
    }

    public class SandraVictimEg : LegalPerson, IVictim
    {
        public SandraVictimEg() : base("SANDRA VICTIM") {  }
    }
}
