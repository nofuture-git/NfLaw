﻿using System;
using System.Collections.Generic;
using NoFuture.Law.Contract.US;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using NUnit.Framework;

namespace NoFuture.Law.Contract.Tests.MutualAssentTests
{
    /// <summary>
    /// LUCY v. ZEHMER Supreme Court of Virginia 196 Va. 493; 84 S.E.2d 516 (1954)
    /// </summary>
    [TestFixture]
    public class LucyvZehmerTests
    {
        [Test]
        public void LucyvZehmer()
        {
            var testSubject = new ComLawContract<Promise>
            {
                Assent = new BarTab(),
                Offer = new FergusonFarm(),
                Acceptance = theFarm => theFarm is FergusonFarm ? new FiftyThousandUsd() : null,
            };

            testSubject.Consideration = new Consideration<Promise>(testSubject)
            {
                //its here, whatever Mr. Lucy is 'thinking' is irrelevant 
                IsSoughtByOfferor = (lp, promise) => lp is WOLucy && promise is FiftyThousandUsd,
                IsGivenByOfferee = (lp, promise) => lp is AHZehmer && promise is FergusonFarm
            };

            var testResult = testSubject.IsValid(new WOLucy(), new AHZehmer());
            Console.WriteLine(string.Join(", ", testSubject.GetReasonEntries()));
            Console.WriteLine(string.Join(", ", testSubject.Assent.GetReasonEntries()));
            Assert.IsTrue(testResult);
        }
    }

    /// <summary>
    /// This is what A.H. Zehmer had in-hand on the night of Dec. 20, 1952
    /// </summary>
    public class BarTab : MutualAssent
    {
        public string Handwrittern =>
            "We hereby agree to sell to W.O. Lucy the Ferguson Farm complete for $50,000.00, title satisfactory to buyer";

        public BarTab()
        {
            //does every body understand "Ferguson Farm" to be tract of land
            // and what "$50,000" is
            TermsOfAgreement = lp =>
            {
                var isParty =    lp is WOLucy 
                              || lp is JCLucy
                              || lp is AHZehmer
                              || lp is IdaSZehmer;
                if (!isParty)
                    return null;

                switch (lp)
                {
                    case WOLucy woLucy:
                        return woLucy.GetTerms();
                    case JCLucy jcLucy:
                        return jcLucy.GetTerms();
                    case AHZehmer ahZehmer:
                        return ahZehmer.GetTerms();
                    case IdaSZehmer idaSZehmer:
                        return idaSZehmer.GetTerms();
                }

                return null;
            };

            //did everyone involved do something that they 
            // each understood to mean its "for real"
            IsApprovalExpressed = lp =>
            {
                switch (lp)
                {
                    case WOLucy woLucy:
                        return IsSignedBy(woLucy);
                    case JCLucy jcLucy:
                        return IsSignedBy(jcLucy);
                }

                return lp is AHZehmer || lp is IdaSZehmer;
            };
        }

        /// <summary>
        /// This is the external assent - the subjective nature of
        /// whoever&apos;s mind is meaningless when everybody understood that
        /// signing is the real thing.
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public bool IsSignedBy(ILegalPerson person)
        {
            return person is WOLucy || person is JCLucy;
        }

        /// <summary>
        /// The shared terms of this contract - no confusion on this.
        /// </summary>
        /// <returns></returns>
        public static ISet<Term<object>> GetTerms()
        {
            return new SortedSet<Term<object>>
            {
                //everybody knows what this is 
                new Term<object>("Ferguson Farm", new FergusonFarm()),
                new Term<object>("$50,000", new FiftyThousandUsd())
            };
        }
    }

    public class FiftyThousandUsd : Promise
    {
        public override bool IsValid(params ILegalPerson[] persons)
        {
            return true;
        }

        public override bool IsEnforceableInCourt => true;
        public override bool Equals(object obj)
        {
            return obj is FiftyThousandUsd;
        }

        public override int GetHashCode()
        {
            return 50000;
        }
    }

    /// <summary>
    /// A tract of land in Dinwiddie county containing 471.6 acres
    /// </summary>
    public class FergusonFarm : Promise
    {
        public override bool IsValid(params ILegalPerson[] persons)
        {
            return true;
        }

        public override bool IsEnforceableInCourt => true;
        public override bool Equals(object obj)
        {
            return obj is FergusonFarm;
        }

        public override int GetHashCode()
        {
            return 1;
        }
    }

    public class WOLucy : LegalPerson, IOfferor
    {
        public WOLucy() : base("W.O. Lucy") { }

        public ISet<Term<object>> GetTerms()
        {
            return BarTab.GetTerms();
        }
    }

    public class JCLucy : LegalPerson, IOfferor
    {
        public JCLucy() : base("J.C. Lucy") { }

        public ISet<Term<object>> GetTerms()
        {
            return BarTab.GetTerms();
        }
    }

    public class AHZehmer : LegalPerson, IOfferee
    {
        public AHZehmer() : base("A.H. Zehmer") { }

        public ISet<Term<object>> GetTerms()
        {
            return BarTab.GetTerms();
        }
    }

    public class IdaSZehmer : LegalPerson, IOfferee
    {
        public IdaSZehmer() : base ("Ida S. Zehmer") { }

        public ISet<Term<object>> GetTerms()
        {
            return BarTab.GetTerms();
        }
    }
}
