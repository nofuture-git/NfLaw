﻿using System;
using System.Linq;
using NoFuture.Law.Contract.US;
using NoFuture.Law.Contract.US.Ucc;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Contract.Tests.UccTests
{
    /// <summary>
    /// FOLEY v. DAYTON BANK & TRUST  Court of Appeals of Tennessee 696 S.W.2d 356 (Tenn.Ct.App. 1985).
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// Doctrine issue, merchant in UCC doesn't just mean any commerical enterprise
    /// ]]>
    /// </remarks>
    public class FoleyvDaytonBankTests
    {
        private readonly ITestOutputHelper output;

        public FoleyvDaytonBankTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void FoleyvDaytonBank()
        {
            var testSubject = new DaytonBank();
            var testGoods = new InternationalTranstarTruck();

            var testResult = testSubject.IsSkilledOrKnowledgeableOf(testGoods);
            testSubject.AddReasonEntryRange(testGoods.GetReasonEntries());
            Assert.False(testResult);
            Assert.True(testSubject.GetReasonEntries().Any());
            this.output.WriteLine(testSubject.ToString());
        }
    }

    public class InternationalTranstarTruck : Goods
    {
        public int Year => 1977;
    }

    public class Foley : LegalPerson, IOfferee
    {
        public Foley() : base("Marvin A. Foley, William E. Ball, III, and Johanna M. Foley") { }
    }

    public class DaytonBank : Merchant, IOfferor
    {
        public DaytonBank() : base("Dayton Bank and Trust") { }

        public override Predicate<Goods> IsSkilledOrKnowledgeableOf { get; set; } = goods =>
        {
            if (goods is InternationalTranstarTruck)
            {
                goods.AddReasonEntry("court found that 'are not merchants' - have no knowledge of big trucks.");
            }

            return false;
        };
    }
}
