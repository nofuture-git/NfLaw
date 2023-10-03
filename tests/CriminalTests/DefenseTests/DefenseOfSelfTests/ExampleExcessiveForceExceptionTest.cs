using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Defense;
using NoFuture.Law.Criminal.US.Defense.Justification;
using NoFuture.Law.Criminal.US.Elements;
using NoFuture.Law.Criminal.US.Elements.Act;
using NoFuture.Law.Criminal.US.Elements.Intent.ComLaw;
using NoFuture.Law.Criminal.US.Terms;
using NoFuture.Law.Criminal.US.Terms.Violence;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.DefenseTests.DefenseOfSelfTests
{
    public class ExampleExcessiveForceExceptionTest
    {
        private readonly ITestOutputHelper output;

        public ExampleExcessiveForceExceptionTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ExampleExcessiveForceException()
        {
            var testCrime = new Felony
            {
                ActusReus = new ActusReus
                {
                    IsAction = lp => lp is PattyEg,
                    IsVoluntary = lp => lp is PattyEg
                },
                MensRea = new MaliceAforethought
                {
                    IsKnowledgeOfWrongdoing = lp => lp is PattyEg,
                    IsIntentOnWrongdoing = lp => lp is PattyEg
                }
            };

            var testResult = testCrime.IsValid(new PattyEg());
            Assert.True(testResult);

            var testSubject = new DefenseOfSelf
            {
                IsReasonableFearOfInjuryOrDeath = lp => true,
                Imminence = new Imminence(ExtensionMethods.Defendant)
                {
                    GetResponseTime = lp => lp is PattyEg ? Imminence.NormalReactionTimeToDanger : TimeSpan.Zero,
                },
                Provocation = new Provocation(ExtensionMethods.Defendant)
                {
                    IsInitiatorOfAttack = lp => lp is PaigeEg,
                    IsInitiatorWithdraws = lp => false,
                    IsInitiatorRespondingToExcessiveForce = lp => lp is PattyEg,
                },
                Proportionality = new Proportionality<ITermCategory>(ExtensionMethods.Defendant)
                {
                    GetChoice = lp => lp is PaigeEg ? new DeadlyForce() : new NondeadlyForce(),
                }
            };
            testResult = testSubject.IsValid(new PattyEg(), new PaigeEg());
            this.output.WriteLine(testSubject.ToString());
            Assert.False(testResult);
        }
    }

    public class PattyEg : LegalPerson, IDefendant
    {
        public PattyEg(): base("PATTY") { }
    }

    public class PaigeEg : LegalPerson, IVictim
    {
        public PaigeEg(): base("PAIGE") { }
    }
}
