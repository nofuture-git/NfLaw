﻿using System;
using System.Collections.Generic;
using System.Linq;
using NoFuture.Law.Contract.US;
using NoFuture.Law.Contract.US.Defense.ToPublicPolicy;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using NUnit.Framework;

namespace NoFuture.Law.Contract.Tests.DefenseTests
{
    /// <summary>
    /// WILLIAMS v. WALKER-THOMAS FURNITURE CO. United States Court of Appeals for the District of Columbia Circuit 121 U.S.App.D.C. 315, 350 F.2d 445 (1965)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, unconsionable can be on unreasonably favorable terms alone
    /// ]]>
    /// </remarks>
    [TestFixture()]
    public class WilliamsvWalkerThomFurnitureTests
    {
        [Test]
        public void WilliamsvWalkerThom()
        {
            var testContract = new ComLawContract<Promise>
            {
                Offer = new OfferPurchaseOnCredit(),
                Acceptance = o => o is OfferPurchaseOnCredit ? new AcceptPurchaseOnCredit() : null,
                Assent = new MutualAssent
                {
                    IsApprovalExpressed = lp => true,
                    TermsOfAgreement = lp => GetTerms()
                }
            };

            testContract.Consideration = new Consideration<Promise>(testContract)
            {
                IsGivenByOfferee = (lp, p) => true,
                IsSoughtByOfferor = (lp, p) => true
            };

            var testSubject = new ByUnconscionability<Promise>(testContract)
            {
                IsUnreasonablyFavorableTerms = lp => lp is WalkerThom
            };

            var testResult = testSubject.IsValid(new WalkerThom(), new Williams());
            Console.WriteLine(testSubject.ToString());
            Assert.IsTrue(testResult);
        }

        public static ISet<Term<object>> GetTerms()
        {
            return new HashSet<Term<object>>
            {
                new Term<object>("deceptive legal jargon", "money sweet money"),
            };
        }
    }

    public class OfferPurchaseOnCredit : Promise
    {
        public override bool IsValid(params ILegalPerson[] persons)
        {
            var offeror = persons.FirstOrDefault();
            var offeree = persons.Skip(1).Take(1).FirstOrDefault();
            return (offeror is Williams || offeror is WalkerThom)
                   && (offeree is Williams || offeree is WalkerThom);
        }

        public string UnconscionableClause => "allow repossess all items, even " +
                                              "those long since paid for in full, " +
                                              "if even one item is every late";
    }

    public class AcceptPurchaseOnCredit : OfferPurchaseOnCredit { }

    public class Williams : LegalPerson, IOfferee
    {
        public Williams():base("WILLIAMS") {}
    }

    public class WalkerThom : LegalPerson, IOfferor
    {
        public WalkerThom(): base("WALKER-THOMAS FURNITURE CO.") {}
    }
}
