﻿using System;
using System.Collections.Generic;
using NoFuture.Law.Contract.US;
using NoFuture.Law.Contract.US.Defense.ToFormation;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Contract.Tests.DefenseTests
{
    /// <summary>
    /// ORTELERE v. TEACHERS’ RETIREMENT BOARD New York Court of Appeals 25 N.Y.2d 196, 250 N.E.2d 460, 303 N.Y.S.2d 362 (1969)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, being considered mentally incompetent makes a contract voidable
    /// ]]>
    /// </remarks>
    public class OrtelerevTeachersBoardTests
    {
        private readonly ITestOutputHelper output;

        public OrtelerevTeachersBoardTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void OrtelerevTeachersBoard()
        {
            var testContract = new ComLawContract<Promise>
            {
                Offer = new MaxInLifetimeOption(),
                Acceptance = o => o as MaxInLifetimeOption,
                Assent = new MutualAssent
                {
                    IsApprovalExpressed = lp => true,
                    TermsOfAgreement = lp => Ortelere.GetTerms()
                }
            };
            testContract.Consideration = new Consideration<Promise>(testContract)
            {
                IsGivenByOfferee = (lp, p) => true,
                IsSoughtByOfferor = (lp, p) => true
            };
            var testSubject = new ByMentalIncompetent<Promise>(testContract) {IsMentallyIncompetent = lp => lp is Ortelere};
            var testResult = testSubject.IsValid(new Ortelere(), new TeachersBoard());
            Assert.True(testResult);
            this.output.WriteLine(testSubject.ToString());
        }
    }

    public class MaxInLifetimeOption : Promise
    {
        public override bool IsValid(params ILegalPerson[] persons)
        {
            return true;
        }
    }

    public class Ortelere : LegalPerson, IOfferor
    {
        public static ISet<Term<object>> GetTerms()
        {
            return new HashSet<Term<object>>
            {
                new Term<object>("pension", DBNull.Value)
            };
        }

    }

    public class TeachersBoard : LegalPerson, IOfferee
    {
        public TeachersBoard() : base("TEACHERS’ RETIREMENT BOARD") { }
    }
}
