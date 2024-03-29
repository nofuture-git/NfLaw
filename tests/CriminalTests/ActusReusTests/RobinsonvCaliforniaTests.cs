﻿using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.Act;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.ActusReusTests
{
    /// <summary>
    /// 370 U.S. 660 (1962) ROBINSON  v. CALIFORNIA. No. 554. Supreme Court of United States.  Argued April 17, 1962. Decided June 25, 1962.
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, the status of a person does not work as actus rea
    /// ]]>
    /// </remarks>
    public class RobinsonvCaliforniaTests
    {
        private readonly ITestOutputHelper output;

        public RobinsonvCaliforniaTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void RobinsonvCalifornia()
        {
            var testSubject = new ActusReus();

            //court found defendant was indeed addict but hadn't actually done anything
            testSubject.IsVoluntary = lp => true;
            testSubject.IsAction = lp => false;

            var testResult = testSubject.IsValid(new Robinson());
            this.output.WriteLine(testSubject.ToString());
            Assert.False(testResult);
        }
    }

    public class Robinson : LegalPerson, IDefendant
    {
        public Robinson():base("ROBINSON") { }
    }
}
