﻿using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Defense;
using NoFuture.Law.Criminal.US.Elements.AgainstProperty.Damage;
using NoFuture.Law.Criminal.US.Elements.Intent.ComLaw;
using NoFuture.Law.US.Persons;
using NUnit.Framework;

namespace NoFuture.Law.Criminal.Tests.TrespassTests
{
    /// <summary>
    /// In the Matter of V.V.C., No. 04-07-00166 CV (Tex.: Court of Appeals, 2008).
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, defense by technicality
    /// ]]>
    /// </remarks>
    [TestFixture()]
    public class InTheMatterOfVvcTests
    {
        [Test]
        public void InTheMatterOfVcc()
        {
            var testCrime = new Felony
            {
                ActusReus = new Arson
                {
                    IsBurned = p => p?.Name == "Metzger Middle School",
                    IsFireStarter = lp => lp is MinorVvc,
                    SubjectProperty = new LegalProperty("Metzger Middle School"),
                },
                MensRea = new GeneralIntent
                {
                    IsKnowledgeOfWrongdoing = lp => lp is MinorVvc
                }
            };
            var testResult = testCrime.IsValid(new MinorVvc());
            Console.WriteLine(testCrime.ToString());
            Assert.IsTrue(testResult);

            var testDefense = new Technicality
            {
                AssertedFact = new InSanAntonio(),
                ActualFact = new NotInSanAntoino(),
                IsMistaken = (t1, t2) => true
            };

            testResult = testDefense.IsValid();
            Console.WriteLine(testDefense.ToString());
            Assert.IsTrue(testResult);

        }
    }

    public class NotInSanAntoino : TermCategory
    {
        protected override string CategoryName => "NOT IN SAN ANTIONIO";
    }

    public class InSanAntonio : TermCategory
    {
        protected override string CategoryName => "IN SAN ANTIONIO";
    }

    public class MinorVvc : LegalPerson, IDefendant
    {
        public MinorVvc():base("V.V.C.") { }
    }
}
