﻿using System;
using NoFuture.Law.Contract.US;
using NoFuture.Law.Contract.US.Ucc;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using NUnit.Framework;

namespace NoFuture.Law.Contract.Tests.UccTests
{
    /// <summary>
    /// TOUSLEY-BIXLER CONSTRUCTION CO. v.COLGATE ENTERPRISES, INC. Court of Appeals of Indiana 429 N.E.2d 979 (Ind.Ct.App. 1982)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// Doctrine, subtle concept of what is and is not goods according to UCC
    /// ]]>
    /// </remarks>
    [TestFixture]
    public class TousleyBixlervColgateTests
    {
        [Test]
        public void TousleyBixlervColgate()
        {
            var testSubject =
                new UccContract<Goods>
                {
                    Assent = new PurchaseOrder50000CubicFeetClay
                    {
                        IsApprovalExpressed = lp => true
                    },
                    Offer = new Order4EarthenClay(),
                    Acceptance = o => new Pay4EarthenClay()
                };
            var testResult = testSubject.IsValid(new Colgate(), new TousleyBixler());
            Assert.IsFalse(testResult);
            Console.WriteLine(testSubject.ToString());
        }
    }

    public class PurchaseOrder50000CubicFeetClay : Agreement
    {

    }

    public class Pay4EarthenClay : Order4EarthenClay { }

    public class Order4EarthenClay : GoodsInTerra
    {
        public int CubicFeet => 50000;
    }

    public class TousleyBixler : LegalPerson, IOfferee
    {
        public TousleyBixler() : base("TOUSLEY-BIXLER CONSTRUCTION CO.") { }
    }

    public class Colgate : LegalPerson, IOfferor
    {
        public Colgate() : base("COLGATE ENTERPRISES, INC.") { }
    }
}
