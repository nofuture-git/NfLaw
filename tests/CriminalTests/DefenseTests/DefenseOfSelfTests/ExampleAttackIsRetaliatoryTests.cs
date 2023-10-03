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
    public class ExampleAttackIsRetaliatoryTests
    {
        private readonly ITestOutputHelper output;

        public ExampleAttackIsRetaliatoryTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ExampleAttackIsRetaliatory()
        {
            var testCrime = new Felony
            {
                ActusReus = new ActusReus
                {
                    IsVoluntary = lp => lp is DwightEg,
                    IsAction = lp => lp is DwightEg
                },
                MensRea = new MaliceAforethought
                {
                    IsKnowledgeOfWrongdoing = lp => lp is DwightEg,
                    IsIntentOnWrongdoing = lp => lp is DwightEg
                }
            };
            var testResult = testCrime.IsValid(new DwightEg());
            Assert.True(testResult);

            var testSubject = new DefenseOfSelf
            {
                IsReasonableFearOfInjuryOrDeath = lp => true,

                Imminence = new Imminence(ExtensionMethods.Defendant)
                {
                    GetResponseTime = lp => lp is DwightEg ? new TimeSpan(0, 0, 2, 0) : TimeSpan.Zero
                },
                Proportionality = new Proportionality<ITermCategory>(ExtensionMethods.Defendant)
                {
                    //example seems to assume that the fist fight is deadly
                    GetChoice = lp => new DeadlyForce()
                },
                Provocation = new Provocation(ExtensionMethods.Defendant)
                {
                    //example doesn't say who started it
                    IsInitiatorOfAttack = lp => true
                }
            };

            testResult = testSubject.IsValid(new DwightEg(), new AbelEg());
            this.output.WriteLine(testSubject.ToString());
            Assert.False(testResult);
        }
    }

    public class DwightEg : LegalPerson, IDefendant
    {
        public DwightEg() : base("DWIGHT") {}
    }

    public class AbelEg : LegalPerson
    {
        public AbelEg() : base("ABEL") {}
    }
}
