﻿using System;
using System.Collections.Generic;
using System.Linq;
using NoFuture.Law.Contract.US;
using NoFuture.Law.Contract.US.Remedy.MoneyDmg;
using NoFuture.Law.Contract.US.Terms;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using NUnit.Framework;

namespace NoFuture.Law.Contract.Tests.RemedyTests
{
    /// <summary>
    /// HOLLYWOOD FANTASY CORP. v. GABOR United States Court of Appeals for the Fifth Circuit 151 F.3d 203 (5th Cir. 1998)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, Testimony about lost profits must at least be based upon some factual data
    /// ]]>
    /// </remarks>
    [TestFixture]
    public class HollywoodFanvGaborTests
    {
        [Test]
        public void HollywoodFanvGabor()
        {
            var testContract = new ComLawContract<Promise>
            {
                Offer = new OfferMakeAppearance(),
                Acceptance = o => o is OfferMakeAppearance ? new AcceptanctMakeAppearance() : null,
                Assent = new MutualAssent
                {
                    IsApprovalExpressed = lp => true,
                    TermsOfAgreement = lp =>
                    {
                        switch (lp)
                        {
                            case HollywoodFan _:
                                return ((HollywoodFan)lp).GetTerms();
                            case Gabor _:
                                return ((Gabor)lp).GetTerms();
                            default:
                                return null;
                        }
                    }
                }
            };

            testContract.Consideration = new Consideration<Promise>(testContract)
            {
                IsGivenByOfferee = (lp, p) => true,
                IsSoughtByOfferor = (lp, p) => true
            };

            var testResult = testContract.IsValid(new HollywoodFan(), new Gabor());
            Assert.IsTrue(testResult);

            var testSubject = new Reliance<Promise>(testContract)
            {
                CalcPrepExpenditures = lp => (lp as HollywoodFan)?.TotalExpense ?? 0m
            };

            testResult = testSubject.IsValid(new HollywoodFan(), new Gabor());
            Console.WriteLine(testSubject.ToString());
            Assert.IsTrue(testResult);
        }
    }

    public class OfferMakeAppearance : Promise
    {
        public override bool IsValid(params ILegalPerson[] persons)
        {
            var offeror = persons.FirstOrDefault();
            var offeree = persons.Skip(1).Take(1).FirstOrDefault();
            return (offeror is HollywoodFan || offeror is Gabor)
                   && (offeree is HollywoodFan || offeree is Gabor);
        }

        public override bool Equals(object obj)
        {
            var o = obj as OfferMakeAppearance;
            if (o == null)
                return false;
            return true;
        }
    }

    public class AcceptanctMakeAppearance : OfferMakeAppearance
    {
        public override bool Equals(object obj)
        {
            var o = obj as AcceptanctMakeAppearance;
            if (o == null)
                return false;
            return true;
        }
    }

    public class HollywoodFan : LegalPerson, IOfferor
    {
        public HollywoodFan(): base("HOLLYWOOD FANTASY CORP.") { }
        public ISet<Term<object>> GetTerms()
        {
            return new HashSet<Term<object>>
            {
                new ContractTerm<object>("celebrity", DBNull.Value),
            };
        }

        public decimal PrintingCost => 8500m;
        public decimal MarketingCost => 12000m;
        public decimal MiscExpenses => 22000;
        public decimal TravelExpense => 9000m;
        public decimal PrepExpense => 6000m;

        public decimal TotalExpense => PrintingCost + MarketingCost + MiscExpenses + TravelExpense + PrepExpense;
    }

    public class Gabor : LegalPerson, IOfferee
    {
        public Gabor(): base("ZSA ZSA GABOR") { }
        public ISet<Term<object>> GetTerms()
        {
            return new HashSet<Term<object>>
            {
                new ContractTerm<object>("celebrity", DBNull.Value),
            };
        }
    }
}
