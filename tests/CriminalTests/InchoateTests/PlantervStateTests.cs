﻿using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.Inchoate;
using NoFuture.Law.Criminal.US.Elements.Intent.ComLaw;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.InchoateTests
{
    /// <summary>
    /// Planter v. State, 9 S.W. 3d 156 (1999)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, with solicitation, which is mostly what is said, understanding the lang is critical
    /// ]]>
    /// </remarks>
    public class PlantervStateTests
    {
        private readonly ITestOutputHelper output;

        public PlantervStateTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void PlantervState()
        {
            var testCrime = new Felony
            {
                ActusReus = new Solicitation
                {
                    IsInduceAnotherToCrime = lp =>
                    {
                        //defendant want to get paid to make the hit, not pay for it
                        var wasSolicited = Planter.GetSolicitation(lp);
                        return string.Equals(wasSolicited, "you do it");
                    }
                },
                MensRea = new SpecificIntent
                {
                    IsKnowledgeOfWrongdoing = lp => lp is Planter
                }
            };
            var testResult = testCrime.IsValid(new Planter());
            this.output.WriteLine(testCrime.ToString());
            Assert.False(testResult);
        }
    }

    public class Planter : LegalPerson, IDefendant
    {
        public Planter(): base("WILLIAM EDWARD PLANTER") { }

        public static string GetSolicitation(ILegalPerson lp)
        {
            if (lp is Planter)
                return "I'll do it";
            if (lp is Government)
                return "you do it";
            return null;
        }
    }
}
