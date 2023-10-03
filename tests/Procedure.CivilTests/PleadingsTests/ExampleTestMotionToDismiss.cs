﻿using System;
using NoFuture.Law.Procedure.Civil.US.Jurisdiction;
using NoFuture.Law.Procedure.Civil.US.Pleadings;
using NoFuture.Law.US.Courts;
using NUnit.Framework;

namespace NoFuture.Law.Procedure.Civil.Tests.PleadingsTests
{
    [TestFixture]
    public class ExampleTestMotionToDismiss
    {
        [Test]
        public void TestMotionToDismissIsValid()
        {
            var testSubject = new PreAnswerMotion
            {
                Court = new StateCourt("CO"),
                GetAssertion = lp => new ExampleCauseForAction(),
                LinkedTo = new Complaint
                {
                    GetRequestedRelief = lp => new ExampleRequestRelief(),
                },
                IsReliefCanBeGranted = lc => lc is ExampleRequestRelief,
                IsSigned = lp => true,
                Jurisdiction = new ExampleJurisdictionIsFalse()
            };

            var testResult = testSubject.IsValid(new ExamplePlaintiff(), new ExampleDefendant());
            Console.WriteLine(testSubject.ToString());
            Assert.IsTrue(testResult);

            testSubject = new PreAnswerMotion
            {
                Court = new StateCourt("CO"),
                GetAssertion = lp => new ExampleCauseForAction(),
                LinkedTo = new Complaint
                {
                    GetRequestedRelief = lp => new ExampleRequestRelief(),
                },
                IsReliefCanBeGranted = lc => false,
                IsSigned = lp => true,
                Jurisdiction = new ExampleJurisdictionIsTrue()
            };

            testResult = testSubject.IsValid(new ExamplePlaintiff(), new ExampleDefendant());
            Console.WriteLine(testSubject.ToString());
            Assert.IsTrue(testResult);

            testSubject = new PreAnswerMotion
            {
                Court = new StateCourt("CO"),
                GetAssertion = lp => new ExampleCauseForAction(),
                LinkedTo = new Complaint
                {
                    GetRequestedRelief = lp => new ExampleRequestRelief(),
                },
                IsReliefCanBeGranted = lc => lc is ExampleRequestRelief,
                IsSigned = lp => true,
                Jurisdiction = new ExampleJurisdictionIsTrue()
            };

            testResult = testSubject.IsValid(new ExamplePlaintiff(), new ExampleDefendant());
            Console.WriteLine(testSubject.ToString());
            Assert.IsFalse(testResult);
        }
    }

    public class ExampleJurisdictionIsFalse : LegalConcept, IJurisdiction
    {
        public override bool IsValid(params ILegalPerson[] persons)
        {
            return false;
        }

        public ICourt Court { get; set; }
    }

    public class ExampleJurisdictionIsTrue : LegalConcept, IJurisdiction
    {
        public override bool IsValid(params ILegalPerson[] persons)
        {
            return true;
        }

        public ICourt Court { get; set; }
    }
}
