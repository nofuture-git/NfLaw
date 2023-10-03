﻿using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.Homicide;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.HomicideTests
{
    /// <summary>
    /// Stevens v. State, 691 N.E.2d 412 (1997)
    /// </summary>
    /// <remarks>
    /// <![CDATA[ doctrine issue, adequate provocation cannot be just words and cannot be due to fear of prosecution ]]>
    /// </remarks>
    public class StevensvStateTests
    {
        private readonly ITestOutputHelper output;

        public StevensvStateTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void StevensvState()
        {
            var testCrime = new Felony
            {
                ActusReus = new ManslaughterVoluntary
                {
                    IsCorpusDelicti = lp => lp is Stevens
                },
                MensRea = new AdequateProvocation
                {
                    IsDefendantActuallyProvoked = lp => lp is Stevens,
                    IsVictimSourceOfIncite = lp => lp is Stevens,
                    //a child threatening to "tell on you" to his mom is not reasonable
                    IsReasonableToInciteKilling = lp => false
                }
            };

            var testResult = testCrime.IsValid(new Stevens());
            this.output.WriteLine(testCrime.ToString());
            Assert.False(testResult);
        }
    }

    public class Stevens : LegalPerson, IDefendant
    {
        public Stevens() : base("CHRISTOPHER M. STEVENS") { }
    }
}
