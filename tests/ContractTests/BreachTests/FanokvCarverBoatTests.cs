﻿using System;
using System.Collections.Generic;
using System.Linq;
using NoFuture.Law.Contract.US;
using NoFuture.Law.Contract.US.Breach;
using NoFuture.Law.Contract.US.Terms;
using NoFuture.Law.Contract.US.Ucc;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using NUnit.Framework;

namespace NoFuture.Law.Contract.Tests.BreachTests
{
    /// <summary>
    /// FANOK v. CARVER BOAT CORP. United States District Court for the Eastern District of New York 576 F.Supp. 2d 404 (E.D.N.Y. 2008)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, limits to the UCC perfect tender rule, Assent to contract is not the same as acceptance of goods
    /// ]]>
    /// </remarks>
    [TestFixture]
    public class FanokvCarverBoatTests
    {
        [Test]
        public void FanokvCarverBoat()
        {
            var testContract = new UccContract<Goods>
            {
                Offer = new OfferSellMarquisYacht(),
                Acceptance = o => o is OfferSellMarquisYacht ? new AcceptanctSellMarquisYacht() : null,
                Assent = new Agreement
                {
                    //although Fanok acts as though acceptance is false
                    IsApprovalExpressed = lp => lp is CarverBoat || lp is Fanok,
                    TermsOfAgreement = lp =>
                    {
                        switch (lp)
                        {
                            case Fanok _:
                                return ((Fanok)lp).GetTerms();
                            case CarverBoat _:
                                return ((CarverBoat)lp).GetTerms();
                            default:
                                return null;
                        }
                    },
                    IsGoodsInspected = lp => false,
                    IsRejected = lp => true,
                    //because they acted as the owner, acceptance is assured
                    IsAnyActAsOwner = lp => lp is Fanok
                }
            };

            var testResult = testContract.IsValid(new CarverBoat(), new Fanok());
            Console.WriteLine(testContract.ToString());
            Assert.IsTrue(testResult);
            var testSubject = new PerfectTender<Goods>(testContract)
            {
                ActualPerformance = lp =>
                {
                    if (lp is CarverBoat)
                        return new OfferSellMarquisYacht();
                    if (lp is Fanok)
                        return new AcceptanctSellMarquisYacht();
                    return null;
                }
            };
            testResult = testSubject.IsValid(new CarverBoat(), new Fanok());
            Console.WriteLine(testSubject.ToString());
            Assert.IsTrue(testResult);
        }
    }

    public class OfferSellMarquisYacht : Goods
    {
        public override bool IsValid(params ILegalPerson[] persons)
        {
            var offeror = persons.FirstOrDefault();
            var offeree = persons.Skip(1).Take(1).FirstOrDefault();
            return (offeror is Fanok || offeror is CarverBoat)
                   && (offeree is Fanok || offeree is CarverBoat);
        }

        public override bool Equals(object obj)
        {
            var o = obj as OfferSellMarquisYacht;
            if (o == null)
                return false;
            return true;
        }
    }

    public class AcceptanctSellMarquisYacht : OfferSellMarquisYacht
    {
        public override bool Equals(object obj)
        {
            var o = obj as AcceptanctSellMarquisYacht;
            if (o == null)
                return false;
            return true;
        }
    }

    public class Fanok : LegalPerson, IOfferee
    {
        public Fanok(): base("JEFFREY FANOK") { }
        public ISet<Term<object>> GetTerms()
        {
            return new HashSet<Term<object>>
            {
                new ContractTerm<object>("", DBNull.Value),
            };
        }
    }

    public class CarverBoat : LegalPerson, IOfferor
    {
        public CarverBoat(): base("CARVER BOAT CORP.") { }
        public ISet<Term<object>> GetTerms()
        {
            return new HashSet<Term<object>>
            {
                new ContractTerm<object>("", DBNull.Value),
            };
        }
    }
}
