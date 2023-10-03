﻿using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Defense;
using NoFuture.Law.Criminal.US.Elements;
using NoFuture.Law.Criminal.US.Elements.Act;
using NoFuture.Law.Criminal.US.Elements.Intent.ComLaw;
using NoFuture.Law.Criminal.US.Terms;
using NoFuture.Law.Criminal.US.Terms.Violence;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.DefenseTests.DefenseOfOtherTests
{
    public class ExampleDuressDefenseTests
    {
        private readonly ITestOutputHelper output;

        public ExampleDuressDefenseTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ExampleDuressDefense()
        {
            var testCrime = new Felony
            {
                ActusReus = new ActusReus
                {
                    IsVoluntary = lp => lp is BrianEg,
                    IsAction = lp => lp is BrianEg
                },
                MensRea = new GeneralIntent
                {
                    IsKnowledgeOfWrongdoing = lp => lp is BrianEg,
                    IsIntentOnWrongdoing = lp => lp is BrianEg
                }
            };

            var testResult = testCrime.IsValid(new BrianEg());
            Assert.True(testResult);

            var testSubject = new ChoiceThereof<ITermCategory>(ExtensionMethods.Defendant)
            {
                GetChoice = lp =>
                {
                    if (lp is KeishaEg)
                        return new Embezzled();
                    if (lp is BrianEg)
                        return new SeriousBodilyInjury();
                    return null;
                },
                GetOtherPossibleChoices = lp =>
                {
                    if (lp is KeishaEg)
                        return new[] {new SeriousBodilyInjury(),};
                    return null;
                }
            };

            testResult = testSubject.IsValid(new KeishaEg(), new BrianEg());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
        }
    }

    public class Embezzled : TermCategory
    {
        protected override string CategoryName { get; } = "embezzled";
        public override int GetRank()
        {
            return new SeriousBodilyInjury().GetRank() - 1;
        }
    }

    public class KeishaEg : LegalPerson, IDefendant
    {
        public KeishaEg() : base("KEISHA TELLER") { }
    }

    public class BrianEg : LegalPerson, IDefendant
    {
        public BrianEg() : base("BRIAN BANKROBBER") { }
    }
}
