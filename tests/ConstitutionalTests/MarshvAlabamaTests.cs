﻿using System;
using System.Collections.Generic;
using System.Linq;
using NoFuture.Law.Constitutional.US;
using NoFuture.Law.Property.US.FormsOf.InTerra;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Constitutional.Tests
{
    /// <summary>
    /// Marsh v. Alabama 326 U.S. 501 (1946)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// Doctrine issue, property which acts as a public community is protected regardless of
    /// who has title
    /// ]]>
    /// </remarks>
    public class MarshvAlabamaTests
    {
        private readonly ITestOutputHelper output;

        public MarshvAlabamaTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void MarshvAlabama()
        {
            Func<ILegalPerson[], ILegalPerson> chargedWithDeprivation =
                lps => lps.FirstOrDefault(lp => lp is Alabama);
            var testSubject2 = new OperationOfCompanyOwnedTown
            {
                GetPartyChargedWithDeprivation = chargedWithDeprivation,
                IsSourceStateAuthority = lp => lp is Alabama,
                FairlyDescribedAsStateActor = new StateAction2.TestIsStateActor(chargedWithDeprivation)
                {
                    IsTraditionalGovernmentFunction = a => a is OperationOfCompanyOwnedTown
                }
            };

            var testResult2 = testSubject2.IsValid(new Marsh(), new Alabama());
            this.output.WriteLine(testSubject2.ToString());
            Assert.True(testResult2);
        }
    }

    public class OperationOfCompanyOwnedTown : StateAction2
    {

    }

    public class Marsh : LegalPerson, IDefendant
    {
        public Marsh(): base("Marsh") { }
    }

    public class Alabama : LegalPerson, IPlaintiff
    {
        public Alabama(): base("Alabama") { }
    }
}
