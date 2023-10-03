﻿using System;
using System.Linq;
using NoFuture.Law.Contract.US;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Contract.Tests.EstoppelTests
{
    /// <summary>
    /// MAYER v. KING COLA MID-AMERICA, INC. Court of Appeals of Missouri, Eastern District 660 S.W.2d 746 (Mo.Ct.App. 1983)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// Doctrine issue, need both predicates to test as true for consideration substitute to be valid
    /// ]]>
    /// </remarks>
    public class MayervKingColaTests
    {
        private readonly ITestOutputHelper output;

        public MayervKingColaTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void MayervKingCola()
        {
            var testSubject = new ComLawContract<Promise>
            {
                Offer = new OfferEmploymentWithKingCola(),
            };
            testSubject.Consideration = new PromissoryEstoppel<Promise>(testSubject)
            {
                IsOffereePositionWorse = lp => false
            };

            var testResult = testSubject.IsValid(new KingCola(), new Mayer());
            Assert.False(testResult);
            this.output.WriteLine(testSubject.ToString());
        }
    }

    public class OfferEmploymentWithKingCola : Promise
    {
        public override bool IsValid(params ILegalPerson[] persons)
        {
            var offeror = persons.FirstOrDefault();
            var offeree = persons.Skip(1).Take(1).FirstOrDefault();
            return offeror is KingCola && offeree is Mayer;
        }
    }

    public class Mayer : LegalPerson, IOfferee
    {
        public Mayer() : base("Theodore Mayer") { }
    }

    public class KingCola : LegalPerson, IOfferor
    {
        public KingCola() : base("King Cola Mid-America, Inc.") { }
    }
}
