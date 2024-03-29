﻿using System;
using NoFuture.Law.Criminal.US.Elements.AgainstProperty.Theft;
using NoFuture.Law.Criminal.US.Elements.AttendantCircumstances;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.TheftTests
{
    public class ExampleExtortionTests
    {
        private readonly ITestOutputHelper output;

        public ExampleExtortionTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ExampleExtortionAct()
        {
            var testAct = new ByExtortion
            {
                IsTakenPossession = lp => lp is RodneyBlackmailEg,
                Threatening = new ByExtortion.ByThreatening
                {
                    IsToAccuseOfCrime = lp => lp is RodneyBlackmailEg
                },
                SubjectProperty = new LegalProperty("fifteen thousand dollars"){ PropertyValue = dt => 15000m } ,
            };

            var testResult = testAct.IsValid(new RodneyBlackmailEg(), new LindseyDealinEg());
            this.output.WriteLine(testAct.ToString());
            Assert.True(testResult);
        }

        [Fact]
        public void ExampleThreatenHonestlyDue()
        {
            var trent = new TrentThreatenEg();
            var thousandDollars = new LegalProperty("thousand dollars") {IsEntitledTo = lp => lp.IsSamePerson(trent), PropertyValue = dt => 10000m };
            var testAct = new ByExtortion
            {
                IsTakenPossession = lp => lp is TrentThreatenEg,
                Threatening = new ByExtortion.ByThreatening
                {
                    IsToExposeHurtfulSecret = lp => lp is TrentThreatenEg
                },
                SubjectProperty = thousandDollars,
                
            };

            var testResult = testAct.IsValid(new TrentThreatenEg(), new TaraLyingEg());
            this.output.WriteLine(testAct.ToString());
            Assert.False(testResult);
        }

        [Fact]
        public void ExampleCaveBasedOnThreat()
        {
            var testAct = new ByExtortion
            {
                IsTakenPossession = lp => lp is RodneyBlackmailEg,
                Threatening = new ByExtortion.ByThreatening
                {
                    IsToAccuseOfCrime = lp => lp is RodneyBlackmailEg
                },
                SubjectProperty = new LegalProperty("fifteen thousand dollars"){ PropertyValue = dt => 15000m },
                Consent = new VictimConsent
                {
                    IsApprovalExpressed = lp => true,
                    IsCapableThereof = lp => lp is LindseyDealinEg
                }
            };

            var testResult = testAct.IsValid(new RodneyBlackmailEg(), new LindseyDealinEg());
            this.output.WriteLine(testAct.ToString());
            Assert.True(testResult);
        }
    }

    public class RodneyBlackmailEg : LegalPerson, IDefendant
    {
        public RodneyBlackmailEg() : base("RODNEY BLACKMAIL") { }
    }

    public class LindseyDealinEg : LegalPerson, IVictim
    {
        public LindseyDealinEg() : base("LINDSEY DEALIN") { }
    }

    public class TrentThreatenEg : LegalPerson, IDefendant
    {
        public TrentThreatenEg() :base("TRENT THREATEN") {  }
    }

    public class TaraLyingEg : LegalPerson, IVictim
    {
        public TaraLyingEg() : base("TARA LYING") { }
    }
}
