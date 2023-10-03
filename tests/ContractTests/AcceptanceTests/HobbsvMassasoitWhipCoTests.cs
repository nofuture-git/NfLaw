﻿using System;
using System.Collections.Generic;
using System.Linq;
using NoFuture.Law.Contract.US;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using NUnit.Framework;

namespace NoFuture.Law.Contract.Tests.AcceptanceTests
{
    /// <summary>
    /// HOBBS v. MASSASOIT WHIP CO. Supreme Judicial Court of Massachusetts 158 Mass. 194, 33 N.E. 495 (1893)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// [   Standing alone, and unexplained, this proposition might seem to imply that one stranger 
    /// may impose a duty upon another, and make him a purchaser, in spite of himself, by sending 
    /// goods to him, unless he will take the trouble, and be at the expense of notifying the sender 
    /// that he will not buy.]
    /// * there was a standing offer
    ///  - plaintiff was not a stranger to the defendant
    ///  - had sent eelskins in the same way four or five times before
    ///  - [eelskins] had been accepted and paid for
    /// * they were accepted
    ///  - silence [...], 
    ///  - coupled with a retention [...] for an unreasonable time [...] 
    /// ]]>
    /// </remarks>
    [TestFixture]
    public class HobbsvMassasoitWhipCoTests
    {
        [Test]
        public void HobbsvMassasoitWhipCo()
        {
            var testSubject = new ComLawContract<Promise>
            {
                Offer = new OfferEelSkins(),
                Acceptance = o => o is OfferEelSkins ? new AcceptanceByDoNothing() : null,
                Assent = new MutualAssent
                {
                    IsApprovalExpressed = lp =>
                    {
                        var massasoit = lp as MassasoitWhipCo;
                        if (massasoit == null)
                        {
                            return lp is Hobbs;
                        }

                        return massasoit.IsEelSkinsAccepted && !massasoit.IsEelSkinsAttemptedReturned;
                    },
                    TermsOfAgreement = lp => new HashSet<Term<object>> {new Term<object>("undefined", DBNull.Value)}
                }
            };
            testSubject.Consideration = new Consideration<Promise>(testSubject)
            {
                IsSoughtByOfferor = (lp, p) => true,
                IsGivenByOfferee = (lp, p) => true
            };

            var testResult = testSubject.IsValid(new Hobbs(), new MassasoitWhipCo());
            Assert.IsTrue(testResult);
            Console.WriteLine(testSubject.ToString());
        }
    }

    public class OfferEelSkins : Promise
    {
        public override bool IsValid(params ILegalPerson[] persons)
        {
            var offeror = persons.FirstOrDefault();
            var offeree = persons.Skip(1).Take(1).FirstOrDefault();
            return offeror is Hobbs && offeree is MassasoitWhipCo;
        }
    }

    public class AcceptanceByDoNothing : Promise
    {
        public override bool IsValid(params ILegalPerson[] persons)
        {
            var offeror = persons.FirstOrDefault();
            var offeree = persons.Skip(1).Take(1).FirstOrDefault();
            return offeror is Hobbs && offeree is MassasoitWhipCo;
        }
    }

    public class Hobbs : LegalPerson, IOfferor
    {

    }
    public class MassasoitWhipCo : LegalPerson, IOfferee
    {
        public bool IsStranger(ILegalPerson lp)
        {
            return !(lp is Hobbs);
        }

        public bool IsStandingOfferWith(ILegalPerson lp)
        {
            return lp is Hobbs;
        }

        public bool IsEelSkinsAccepted => true;
        public bool IsEelSkinsAttemptedReturned => false;
    }
}
