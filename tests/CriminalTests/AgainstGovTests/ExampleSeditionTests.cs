﻿using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.AgainstGov;
using NoFuture.Law.Criminal.US.Elements.Intent.PenalCode;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.AgainstGovTests
{
    public class ExampleSeditionTests
    {
        private readonly ITestOutputHelper output;

        public ExampleSeditionTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestSedition()
        {
            var testCrime = new Felony
            {
                ActusReus = new Sedition
                {
                    IsByThreatOfViolence = lp => lp is MoDisgruntledEg,
                    IsToOverthrowGovernment = lp => lp is MoDisgruntledEg,
                    IsInWrittenForm = lp => lp is MoDisgruntledEg
                },
                MensRea = new Purposely
                {
                    IsIntentOnWrongdoing = lp => lp is MoDisgruntledEg
                }
            };
            var testResult = testCrime.IsValid(new MoDisgruntledEg());
            this.output.WriteLine(testCrime.ToString());
            Assert.True(testResult);
        }
    }

    public class MoDisgruntledEg : LegalPerson, IDefendant
    {
        public MoDisgruntledEg(string name) : base(name) {  }
        public MoDisgruntledEg(): base("MO DISGRUNTLED") {  }
    }
}
