﻿using System;
using System.Linq;
using NoFuture.Law.Contract.US;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using NUnit.Framework;

namespace NoFuture.Law.Contract.Tests.EstoppelTests
{
    /// <summary>
    /// CONRAD v. FIELDS Court of Appeals of Minnesota 2007 Minn.App.Unpub.LEXIS 744
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// Doctrine issue, promissory estoppel attempts to return the relying promisee 
    /// back into pre-contractual state
    /// ]]>
    /// </remarks>
    [TestFixture]
    public class ConradvFieldsTests
    {
        [Test]
        public void ConradvFields()
        {
            var testSubject = new ComLawContract<Promise>()
            {
                Offer = new OfferPayForLawSchool()
            };
            testSubject.Consideration = new PromissoryEstoppel<Promise>(testSubject)
            {
                IsOffereeDependedOnPromise = lp => true,
                IsOffereePositionWorse = lp => true
            };
            var testResult = testSubject.IsValid(new Fields(), new Conrad());
            Assert.IsTrue(testResult);
            Console.WriteLine(testSubject.ToString());
        }
    }

    public class OfferPayForLawSchool : Promise
    {
        public override bool IsValid(params ILegalPerson[] persons)
        {
            var offeror = persons.FirstOrDefault();
            var offeree = persons.Skip(1).Take(1).FirstOrDefault();
            return offeror is Fields && offeree is Conrad;
        }
    }

    public class Conrad : LegalPerson, IOfferee
    {
        public Conrad() : base("Marjorie Conrad") { }
    }

    public class Fields : LegalPerson, IOfferor
    {
        public Fields() : base("Walter R. Fields") { }
    }
}
