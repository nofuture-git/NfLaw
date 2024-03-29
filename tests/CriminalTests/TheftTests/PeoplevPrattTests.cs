﻿using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.AgainstProperty.Theft;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.TheftTests
{
    /// <summary>
    /// People v. Pratt, 656 N.W.2d 866 (2002)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, stolen means taking witout permission, intenting to return it doesn't make it anything less
    /// ]]>
    /// </remarks>
    public class PeoplevPrattTests
    {
        private readonly ITestOutputHelper output;

        public PeoplevPrattTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void PeoplevPratt()
        {
            var testCrime = new Felony
            {
                ActusReus = new ByReceiving
                {
                    
                    IsTakenPossession = lp => lp is Pratt,
                    SubjectProperty = new LegalProperty("1990 Buick Regal")
                    {
                        IsEntitledTo = lp => lp is PrattFormerGirlfriend,
                        IsInPossessionOf = lp => lp is PrattFormerGirlfriend,
                        PropertyValue = dt => 1000.99m
                    },
                    //court finds its stolen, by statute, regardless of intent-to-return
                    IsPresentStolen = lp => lp is Pratt
                }
            };

            var testResult = testCrime.IsValid(new Pratt(), new PrattFormerGirlfriend());
            this.output.WriteLine(testCrime.ToString());
            Assert.True(testResult);
        }
    }

    public class Pratt : LegalPerson, IDefendant
    {
        public Pratt(): base("EDDIE JUNIOR PRATT") { }
    }

    public class PrattFormerGirlfriend : LegalPerson, IVictim
    {
        public PrattFormerGirlfriend(): base("FORMER GIRLFRIEND") {  }
    }
}
