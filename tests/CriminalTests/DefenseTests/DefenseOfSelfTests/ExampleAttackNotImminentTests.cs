using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Defense;
using NoFuture.Law.Criminal.US.Defense.Justification;
using NoFuture.Law.Criminal.US.Elements;
using NoFuture.Law.Criminal.US.Elements.Act;
using NoFuture.Law.Criminal.US.Elements.Intent.PenalCode;
using NoFuture.Law.Criminal.US.Terms;
using NoFuture.Law.Criminal.US.Terms.Violence;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.DefenseTests.DefenseOfSelfTests
{
    public class ExampleAttackNotImminentTests
    {
        private readonly ITestOutputHelper output;

        public ExampleAttackNotImminentTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ExampleAttackNotImminent()
        {
            var testCrime = new Felony
            {
                ActusReus = new ActusReus
                {
                    IsAction = lp => lp is FionaEg,
                    IsVoluntary = lp => lp is FionaEg
                },
                MensRea = new Knowingly
                {
                    IsIntentOnWrongdoing = lp => lp is FionaEg,
                    IsKnowledgeOfWrongdoing = lp => lp is FionaEg
                }
            };

            var testResult = testCrime.IsValid(new FionaEg());
            Assert.True(testResult);

            var testSubject = new DefenseOfSelf
            {
                IsReasonableFearOfInjuryOrDeath = lp => true,
                Imminence = new Imminence(ExtensionMethods.Defendant)
                {
                    GetResponseTime = lp =>
                        lp is FionaEg || lp is VinnyEg ? new TimeSpan(365, 0, 0, 0) : TimeSpan.Zero
                },
                Proportionality = new Proportionality<ITermCategory>(ExtensionMethods.Defendant)
                {
                    GetChoice = lp => new DeadlyForce(),
                },
                Provocation = new Provocation(ExtensionMethods.Defendant)
                {
                    IsInitiatorOfAttack = lp => lp is VinnyEg,
                }
            };

            testResult = testSubject.IsValid(new FionaEg(), new VinnyEg());
            this.output.WriteLine(testSubject.ToString());
            Assert.False(testResult);
        }
    }

    public class VinnyEg : LegalPerson
    {
        public VinnyEg() : base("VINNY") {}
    }

    public class FionaEg : LegalPerson, IDefendant
    {
        public FionaEg(): base("FIONA") { }
    }
}
