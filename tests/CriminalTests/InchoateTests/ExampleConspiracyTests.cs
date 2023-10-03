using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.Inchoate;
using NoFuture.Law.Criminal.US.Elements.Intent.ComLaw;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.InchoateTests
{
    public class ExampleConspiracyTests
    {
        private readonly ITestOutputHelper output;

        public ExampleConspiracyTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ExampleConspiracyActNoOvert()
        {
            var testSubject = new Conspiracy
            {
                IsAgreementToCommitCrime = lp => lp is MelissaEg,
                IsOvertActRequired = null
            };

            var testResult = testSubject.IsValid(new MelissaEg());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
        }

        [Fact]
        public void ExampleConspiracyActOvert()
        {
            var testSubject = new Conspiracy
            {
                IsAgreementToCommitCrime = lp => lp is MelissaEg,
                IsOvertActRequired = lp => lp is MelissaEg
            };

            var testResult = testSubject.IsValid(new MelissaEg());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
        }

        [Fact]
        public void ExampleRequiresSpecificIntent()
        {
            var testCrime = new Felony
            {
                ActusReus = new Conspiracy
                {
                    IsAgreementToCommitCrime = lp => lp is MelissaEg
                },
                MensRea = new GeneralIntent
                {
                    IsKnowledgeOfWrongdoing = lp => lp is MelissaEg,
                    IsIntentOnWrongdoing = lp => lp is MelissaEg
                }
            };

            var testResult = testCrime.IsValid(new MelissaEg());
            this.output.WriteLine(testCrime.ToString());
            Assert.False(testResult);
        }

        [Fact]
        public void ExamplePinkertonRuleTest()
        {
            var testCrime = new Felony
            {
                ActusReus = new PinkertonRule(new Conspiracy
                {
                    IsAgreementToCommitCrime = lp => lp is ShellyDriverEg,
                }),
                MensRea = new SpecificIntent
                {
                    IsIntentOnWrongdoing = lp => lp is ShellyDriverEg || lp is SamRobberEg
                },
            };

            var testResult = testCrime.IsValid(new ShellyDriverEg());
            this.output.WriteLine(testCrime.ToString());
            Assert.True(testResult);
        }

        [Fact]
        public void TestWhartonRule()
        {
            var testCrime = new Felony
            {
                ActusReus = new Conspiracy
                {
                    IsAgreementToCommitCrime = lp => lp is ShellyDriverEg,
                    IsConcertOfAction = true
                },
                MensRea = new SpecificIntent
                {
                    IsKnowledgeOfWrongdoing = lp => lp is ShellyDriverEg,
                    IsIntentOnWrongdoing = lp => lp is ShellyDriverEg
                }
            };
            var testResult = testCrime.IsValid(new ShellyDriverEg());
            this.output.WriteLine(testCrime.ToString());
            Assert.False(testResult);
        }
    }

    public class ShellyDriverEg : LegalPerson, IDefendant
    {
        public ShellyDriverEg() : base("SHELLY DRIVER") { }
    }

    public class SamRobberEg : LegalPerson, IDefendant
    {
        public SamRobberEg() : base("SAM ROBBER") { }
    }
}
