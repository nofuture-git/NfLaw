﻿using System;
using NoFuture.Law;
using NoFuture.Law.Procedure.Civil.US.Jurisdiction;
using NoFuture.Law.US;
using NoFuture.Law.US.Courts;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Procedure.Civil.Tests
{
    /// <summary>
    /// Civil Procedure Examples &amp; Explanations 8th Ed. Joseph W. Glannon pg 12
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// 
    /// ]]>
    /// </remarks>
    public class ExampleSmokyMountainVWTests
    {
        private readonly ITestOutputHelper output;

        public ExampleSmokyMountainVWTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void HudsonvSmokyMountainVW()
        {
            var testSubject = new PersonalJurisdiction(new StateCourt("Ohio"))
            {
                Consent = Consent.NotGiven(),
                //this fires true when the defendant lives in the state of injury
                GetDomicileLocation = lp => lp is Hudson ? new VocaBase("Ohio") : new VocaBase("North Carolina"),
                GetInjuryLocation = lp => lp is Hudson ? new VocaBase("Ohio") : null,
                GetCurrentLocation = lp => lp is Hudson ? new VocaBase("Ohio") : new VocaBase("North Carolina"),
                MinimumContact = new MinimumContact(new StateCourt("Ohio"))
                {
                    //the dealership sold the car, not entered into a contract to finance it
                    GetCommerciallyEngagedLocation = lp => lp is SmokyMountainVW ? new[]{ new VocaBase("North Carolina")} : null,
                    //Hudson went to NC and bought the car
                    GetDirectedContactTo = lp => lp is Hudson ? new SmokyMountainVW() : null
                }

            };

            var testResult = testSubject.IsValid(new Hudson(), new SmokyMountainVW());
            this.output.WriteLine(testSubject.ToString());
            Assert.False(testResult);
        }

        [Fact]
        public void FordvSmokyMountainVW()
        {
            const string FL = "Florida";
            const string NC = "North Carolina";

            var testSubject = new PersonalJurisdiction(new StateCourt(FL))
            {
                Consent = Consent.NotGiven(),
                GetDomicileLocation =
                    lp => lp is FloridaFord ? new VocaBase(FL) : new VocaBase(NC),
                GetInjuryLocation = lp => lp is FloridaFord ? new VocaBase(FL) : null,
                MinimumContact = new MinimumContact(new StateCourt(FL))
                {
                    GetCommerciallyEngagedLocation = lp =>
                        lp is SmokyMountainVW ? new[] {new VocaBase(NC)} : null,
                    //Smoky Mtn sold Florida Ford a rigged car 
                    GetIntentionalTortTo = lp => lp is SmokyMountainVW ? new FloridaFord() : null
                }
            };

            var testResult = testSubject.IsValid(new FloridaFord(), new SmokyMountainVW());
            Assert.True(testResult);
            this.output.WriteLine(testSubject.ToString());
        }

        [Fact]
        public void PackardvSmokyMountainVW()
        {
            const string PA = "Pennsylvania";
            const string NC = "North Carolina";

            var testSubject = new PersonalJurisdiction(new StateCourt(PA))
            {
                Consent = Consent.NotGiven(),
                GetDomicileLocation = lp => lp is PennsylvaniaPackard ? new VocaBase(PA) : new VocaBase(NC),
                GetInjuryLocation = lp => lp is PennsylvaniaPackard ? new VocaBase(PA) : null,
                MinimumContact = new MinimumContact(new StateCourt(PA))
                {
                    GetCommerciallyEngagedLocation = lp => lp is SmokyMountainVW ? new[] {new VocaBase(NC)} : null,
                    //because the dealership reached out to the Plaintiff in PA, PA has jurisdiction
                    GetDirectedContactTo = lp => lp is SmokyMountainVW ? new PennsylvaniaPackard() : null
                }
            };

            var testResult = testSubject.IsValid(new PennsylvaniaPackard(), new SmokyMountainVW());
            Assert.True(testResult);
            this.output.WriteLine(testSubject.ToString());

        }
    }

    public class Hudson : LegalPerson, IPlaintiff
    {
        public Hudson(): base("Hudson") { }
    }

    public class SmokyMountainVW : LegalPerson, IDefendant
    {
        public SmokyMountainVW(): base("Smoky Mountain VW") { }
    }

    public class FloridaFord : LegalPerson, IPlaintiff
    {
        public FloridaFord() : base("Florida Ford") { }
    }

    public class PennsylvaniaPackard : LegalPerson, IPlaintiff
    {
        public PennsylvaniaPackard() : base("Pennsylvania Packard") { }
    }
}
