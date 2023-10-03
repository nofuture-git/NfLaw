﻿using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.AgainstProperty.Theft;
using NoFuture.Law.Criminal.US.Elements.AttendantCircumstances;
using NoFuture.Law.Criminal.US.Elements.Intent.ComLaw;
using NoFuture.Law.Property.US.FormsOf;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using NUnit.Framework;

namespace NoFuture.Law.Criminal.Tests.TheftTests
{
    /// <summary>
    /// State v. Larson, 605 N.W. 2d 706 (2000)
    /// </summary>
    /// <remarks>
    ///<![CDATA[
    /// doctrine issue, security deposit is a debt and a person cannot be put in jail for not paying a debt.
    /// ]]>
    /// </remarks>
    [TestFixture()]
    public class StatevLarsonTests
    {
        [Test]
        public void StatevLarson()
        {
            var securityDeposit = new SecurityDeposit();
            var larson = new Larson();
            var lessee = new TheLesseeEg();
            var kindOrRelationship = Larson.GetKindOfRelationship();
            switch (kindOrRelationship)
            {
                //the deposit is a debt 
                case "debtor-creditor":
                    securityDeposit.IsAllowedToCommingle = true;
                    securityDeposit.IsEntitledTo = lp => lp.IsSamePerson(larson);
                    break;
                case "pledgor-pledgee":
                    securityDeposit.IsAllowedToCommingle = true;
                    securityDeposit.IsEntitledTo = lp => lp.IsSamePerson(lessee);
                    break;
                case "settlor-trustee":
                    securityDeposit.IsAllowedToCommingle = false;
                    securityDeposit.IsEntitledTo = lp => lp.IsSamePerson(lessee);
                    break;
            }

            var testCrime = new Felony
            {
                ActusReus = new ByTaking
                {
                    SubjectProperty = securityDeposit,
                    Consent = new VictimConsent
                    {
                        IsCapableThereof = lp => true,
                        IsApprovalExpressed = lp => true,
                    },
                    IsTakenPossession = lp => lp is Larson,
                    IsAsportation = lp => lp is Larson
                },
                MensRea = new GeneralIntent
                {
                    IsIntentOnWrongdoing = lp => lp is Larson
                }
            };

            var testResult = testCrime.IsValid(larson, lessee);
            Console.WriteLine(testCrime.ToString());
            Assert.IsFalse(testResult);

        }
    }

    public class TheLesseeEg : LegalPerson
    {
        public TheLesseeEg() : base("LESSEE") {  }
    }

    public class SecurityDeposit : PersonalProperty
    {
        public bool IsAllowedToCommingle { get; set; }
    }

    public class Larson : LegalPerson, IDefendant
    {
        public Larson() : base("FRANK DONALD LARSON") { }

        public static  string GetKindOfRelationship()
        {
            return "debtor-creditor";
        }
    }
}
