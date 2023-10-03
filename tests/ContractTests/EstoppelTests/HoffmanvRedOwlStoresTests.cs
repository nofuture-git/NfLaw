using System;
using System.Linq;
using NoFuture.Law.Contract.US;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Contract.Tests.EstoppelTests
{
    /// <summary>
    /// HOFFMAN v. RED OWL STORES, INC. Supreme Court of Wisconsin 26 Wis. 2d 683, 133 N.W.2d 267 (1965)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// Doctrine issue, consideration substitute of promissory estoppel 
    /// applicable to a promise that is not donative
    /// ]]>
    /// </remarks>
    public class HoffmanvRedOwlStoresTests
    {
        private readonly ITestOutputHelper output;

        public HoffmanvRedOwlStoresTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void HoffmanvRedOwlStores()
        {
            var testSubject = new ComLawContract<Promise>()
            {
                Offer = new OfferRedOwlFranchise(),
                Acceptance = o => o is OfferRedOwlFranchise ? new AcceptanceRedOwlFranchise() : null,
            };
            testSubject.Consideration = new PromissoryEstoppel<Promise>(testSubject)
            {
                IsOffereeDependedOnPromise = lp => lp is Hoffman,
                IsOffereePositionWorse = lp => lp is Hoffman
            };

            var testResult = testSubject.IsValid(new RedOwlStores(), new Hoffman());
            Assert.True(testResult);
            this.output.WriteLine(testSubject.ToString());
        }
    }

    public class OfferRedOwlFranchise : Promise
    {
        public override bool IsValid(params ILegalPerson[] persons)
        {
            var offeror = persons.FirstOrDefault();
            var offeree = persons.Skip(1).Take(1).FirstOrDefault();
            return offeror is RedOwlStores && offeree != null;
        }

        public decimal ExpectedPrice => 18000m;
        public decimal ActualPrice = 24300m;
    }

    public class AcceptanceRedOwlFranchise : Promise
    {
        public override bool IsValid(params ILegalPerson[] persons)
        {
            var offeror = persons.FirstOrDefault();
            var offeree = persons.Skip(1).Take(1).FirstOrDefault();
            return offeror is RedOwlStores && offeree is Hoffman;
        }
    }

    public class Hoffman : LegalPerson, IOfferee
    {
        public Hoffman() : base("Joseph Hoffman") { }
    }

    public class RedOwlStores : LegalPerson, IOfferor
    {
        public RedOwlStores() :base("Red Owl Stores") { }
    }
}
