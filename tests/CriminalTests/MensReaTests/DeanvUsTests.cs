﻿using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.Act;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using NUnit.Framework;

namespace NoFuture.Law.Criminal.Tests.MensReaTests
{
    /// <summary>
    /// Dean v. United States, 556 U.S. 568 (2009)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, mens rea is not required because the accident only 
    /// occured because Dean was already involved in an unlawful act.
    /// ]]>
    /// </remarks>
    [TestFixture]
    public class DeanvUsTests
    {
        [Test]
        public void DeanvUs()
        {
            var testSubject = new Felony
            {
                ActusReus = new ActusReus
                {
                    IsVoluntary = lp => lp is Dean,
                    IsAction = lp => lp is Dean
                },
            };

            var testResult = testSubject.ActusReus.IsValid(new Dean());
            Assert.IsTrue(testResult);
        }
    }

    public class Dean : LegalPerson, IDefendant
    {

    }
}
