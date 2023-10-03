﻿using System;
using System.Collections.Generic;
using System.Linq;
using NoFuture.Law.Contract.US;
using NoFuture.Law.Contract.US.Terms;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using NUnit.Framework;

namespace NoFuture.Law.Contract.Tests.SemiosisTests
{
    /// <summary>
    /// MENDENHALL v. HANESBRANDS, INC. U.S.District Court for the Middle District of North Carolina 856 F.Supp. 2d 717 (M.D.N.C. 2012)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, default implied terms of good faith and fair dealings 
    /// ]]>
    /// </remarks>
    [TestFixture]
    public class MendenhallvHanesbrandsTests
    {
        [Test]
        public void MendenhallvHanesBrands()
        {
            var testContract = new ComLawContract<Promise>
            {
                Offer = new OfferPromoteChampion(),
                Acceptance = o => o is OfferPromoteChampion ? new AcceptancePromoteChampion() : null,
                Assent = new MutualAssent
                {
                    IsApprovalExpressed = lp => true,
                    TermsOfAgreement = lp =>
                    {
                        switch (lp)
                        {
                            case Mendenhall _:
                                return ((Mendenhall)lp).GetTerms();
                            case HanesBrands _:
                                return ((HanesBrands)lp).GetTerms();
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

            var testResult = testContract.IsValid(new Mendenhall(), new HanesBrands());
            Console.WriteLine(testContract.ToString());
            Assert.IsTrue(testResult);
        }
    }

    public class OfferPromoteChampion : Promise
    {
        public override bool IsValid(params ILegalPerson[] persons)
        {
            var offeror = persons.FirstOrDefault();
            var offeree = persons.Skip(1).Take(1).FirstOrDefault();
            return (offeror is Mendenhall || offeror is HanesBrands)
                && (offeree is Mendenhall || offeree is HanesBrands);
        }
    }

    public class AcceptancePromoteChampion : OfferPromoteChampion
    {
    }

    public class Mendenhall : LegalPerson, IOfferor
    {
        public Mendenhall() : base("RASHARD MENDENHALL") { }
        public ISet<Term<object>> GetTerms()
        {
            return new HashSet<Term<object>>
            {
                new ContractTerm<object>("morals clause", DBNull.Value),
                new ContractTerm<object>("covenant of good faith", 2)
            };
        }
    }

    public class HanesBrands : LegalPerson, IOfferee
    {
        public HanesBrands() : base("HANESBRANDS, INC.") { }
        public ISet<Term<object>> GetTerms()
        {
            return new HashSet<Term<object>>
            {
                new ContractTerm<object>("morals clause", DBNull.Value),
                new ContractTerm<object>("covenant of good faith", 2)
            };
        }
    }
}
