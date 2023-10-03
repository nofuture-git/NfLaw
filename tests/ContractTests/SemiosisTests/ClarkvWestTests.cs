using System;
using System.Collections.Generic;
using System.Linq;
using NoFuture.Law.Contract.US;
using NoFuture.Law.Contract.US.Semiosis;
using NoFuture.Law.Contract.US.Terms;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Contract.Tests.SemiosisTests
{
    /// <summary>
    /// CLARK v. WEST Court of Appeals of New York 193 N.Y. 349, 86 N.E. 1 (1908)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, separation of substance of consideration and ancillary conditionals which may be dismissed (waived)
    /// ]]>
    /// </remarks>
    public class ClarkvWestTests
    {
        private readonly ITestOutputHelper output;

        public ClarkvWestTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ClarkvWest()
        {
            var testContract = new ComLawContract<Promise>
            {
                Offer = new OfferWriteLawDox(),
                Acceptance = o => o is OfferWriteLawDox ? new AcceptanceWriteLawDox() : null,
                Assent = new MutualAssent
                {
                    IsApprovalExpressed = lp => true,
                    TermsOfAgreement = lp =>
                    {
                        switch (lp)
                        {
                            case Clark _:
                                return ((Clark)lp).GetTerms();
                            case West _:
                                return ((West)lp).GetTerms();
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

            var testSubject = new ConditionsPrecedent<Promise>(testContract)
            {
                IsConditionalTerm = t => t.Name.StartsWith("abstain from"),
                IsNotConditionMet = (t, lp) =>
                {
                    var isParty = lp is West;
                    if (!isParty)
                        return false;
                    var isTerm = t.Name.StartsWith("abstain from");
                    if (!isTerm)
                        return false;

                    //they failed to meet this requirement
                    if (isTerm && isParty)
                        return true;
                    return false;
                }
            };

            var testResult = testSubject.IsValid(new Clark(), new West());
            this.output.WriteLine(testSubject.ToString());
            Assert.False(testResult);
        }
    }

    public class OfferWriteLawDox : Promise
    {
        public override bool IsValid(params ILegalPerson[] persons)
        {
            var offeror = persons.FirstOrDefault();
            var offeree = persons.Skip(1).Take(1).FirstOrDefault();
            return (offeror is Clark || offeror is West)
                && (offeree is Clark || offeree is West);
        }
    }
    public class AcceptanceWriteLawDox : OfferWriteLawDox { }

    public class Clark : LegalPerson, IOfferor
    {
        public Clark() : base("WILLIAM LAWRENCE CLARK, JR.") { }
        public ISet<Term<object>> GetTerms()
        {
            return new HashSet<Term<object>>
            {
                new ContractTerm<object>("abstain from the use of intoxicating liquors", DBNull.Value),
            };
        }
    }

    public class West : LegalPerson, IOfferee
    {
        public West() : base("JOHN BRIGGS WEST") { }
        public ISet<Term<object>> GetTerms()
        {
            return new HashSet<Term<object>>
            {
                new ContractTerm<object>("abstain from the use of intoxicating liquors", DBNull.Value),
            };
        }
    }
}
