﻿using System;
using NoFuture.Law.Criminal.US.Defense.Justification;
using NUnit.Framework;
using NoFuture.Law.US.Persons;
using NoFuture.Law.US;
using NoFuture.Law.Criminal.US.Terms;
using NoFuture.Law.Criminal.US.Terms.Violence;

namespace NoFuture.Law.Tort.Tests
{
    /// <summary>
    /// Bird v. Holbrook, 130 Eng. Rep. 911 (C.P. 1825)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue property protection using indiscriminate traps
    /// is not an effective defense since they do not distinguish a thief from a trespassor
    /// ]]>
    /// </remarks>
    [TestFixture]
    public class BirdvHolbrookTests
    {
        [Test]
        public void BirdvHolbrook()
        {
            var test = new DefenseOfProperty(ExtensionMethods.Tortfeasor)
            {
                Proportionality = new Proportionality<ITermCategory>(ExtensionMethods.Tortfeasor)
                {
                    GetChoice = lp =>
                    {
                        if(lp is Holbrook)
                            return new DeadlyForce();
                        return new NondeadlyForce();
                    }
                }
            };

            var testResult = test.IsValid(new Holbrook(), new Bird());
            Console.WriteLine(test.ToString());
            Assert.IsFalse(testResult);
        }
    }

    public class Bird : LegalPerson, IPlaintiff
    {
        public Bird(): base("") { }
    }

    public class Holbrook : LegalPerson, ITortfeasor
    {
        public Holbrook(): base("") { }
    }
}
