﻿using System;
using System.Collections.Generic;
using System.Linq;
using NoFuture.Law.Contract.US;
using NoFuture.Law.Contract.US.Defense.ToAssent;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using NUnit.Framework;

namespace NoFuture.Law.Contract.Tests.DefenseTests
{
    /// <summary>
    /// STANDARD FINANCE CO. v. ELLIS Intermediate Court of Appeals of Hawaii 3 Haw.App. 614, 657 P.2d 1056 (Haw.Ct.App. 1983)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, the specific objective rules of physical and improper threat
    /// ]]>
    /// </remarks>
    [TestFixture]
    public class StandardFinancevEllisTests
    {
        [Test]
        public void StandardFinancevEllis()
        {
            var testContract = new ComLawContract<Promise>
            {
                Offer = new OfferOnPromissoryNote(),
                Acceptance = o => o is OfferOnPromissoryNote ? new AcceptOnPromissoryNote() : null,
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

            var testSubject = new ByDuress<Promise>(testContract);

            //court found that there was not threat made
            var testResult = testSubject.IsValid(new Ellis(), new StandardFinance());
            Assert.IsFalse(testResult);
            Console.WriteLine(testSubject.ToString());

        }
        public static ISet<Term<object>> GetTerms()
        {
            return new HashSet<Term<object>>
            {
                new Term<object>("promissory note", DBNull.Value)
            };
        }
    }

    public class OfferOnPromissoryNote : Promise
    {
        public override bool IsValid(params ILegalPerson[] persons)
        {
            var offeror = persons.FirstOrDefault();
            var offeree = persons.Skip(1).Take(1).FirstOrDefault();
            return (offeror is StandardFinance || offeror is Ellis)
                   && (offeree is StandardFinance || offeree is Ellis);
        }

        public decimal Amount => 2800m;
        public DateTime OnDate => new DateTime(1976,9,30);
    }

    public class AcceptOnPromissoryNote : OfferOnPromissoryNote { }

    public class StandardFinance : LegalPerson, IOfferee
    {
        public StandardFinance() : base("Standard Finance Company, Limited") { }
    }

    public class Ellis : LegalPerson, IOfferor
    {
        public Ellis() : base("Betty Ellis") { }
    }
}
