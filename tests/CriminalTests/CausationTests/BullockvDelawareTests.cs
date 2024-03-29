﻿using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.Act;
using NoFuture.Law.Criminal.US.Elements.Intent.PenalCode;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.CausationTests
{
    /// <summary>
    /// Bullock v. State, 775 A.2d. 1043 (2001)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, intervening superseding cause in that the death of a person was partially caused by their own unlawful actions
    /// ]]>
    /// </remarks>
    public class BullockvDelawareTests
    {
        private readonly ITestOutputHelper output;

        public BullockvDelawareTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void BullockvDelaware()
        {
            var testSubject = new Felony
            {
                ActusReus = new ActusReus
                {
                    IsVoluntary = lp => lp is Bullock,
                    IsAction = lp => lp is Bullock
                },
                MensRea = new Recklessly
                {
                    IsDisregardOfRisk = lp =>
                    {
                        lp.AddReasonEntry("reckless causation was nonetheless not established");
                        return false;
                    },
                    IsUnjustifiableRisk = lp => lp is Bullock
                }

            };

            var testResult = testSubject.IsValid(new Bullock());
            this.output.WriteLine(testSubject.ToString());
            Assert.False(testResult);

        }
    }

    public class Bullock : LegalPerson, IDefendant
    {

    }
}
