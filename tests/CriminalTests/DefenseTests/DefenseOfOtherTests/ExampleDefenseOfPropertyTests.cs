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

namespace NoFuture.Law.Criminal.Tests.DefenseTests.DefenseOfOtherTests
{
    public class ExampleDefenseOfPropertyTests
    {
        private readonly ITestOutputHelper output;

        public ExampleDefenseOfPropertyTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ExampleDefenseOfProperty()
        {
            var testCrime = new Misdemeanor
            {
                ActusReus = new ActusReus
                {
                    IsAction = lp => lp is KelseyEg,
                    IsVoluntary = lp => lp is KelseyEg
                },
                MensRea = new GeneralIntent
                {
                    IsKnowledgeOfWrongdoing = lp => lp is KelseyEg,
                    IsIntentOnWrongdoing = lp => lp is KelseyEg
                }
            };

            var testResult = testCrime.IsValid(new KelseyEg());
            Assert.True(testResult);

            var testSubject = new DefenseOfProperty
            {
                IsBeliefProtectProperty = lp => lp is KelseyEg,
                Imminence = new Imminence(ExtensionMethods.Defendant)
                {
                    GetResponseTime = lp => Imminence.NormalReactionTimeToDanger
                },
                Provocation = new Provocation(ExtensionMethods.Defendant)
                {
                    IsInitiatorOfAttack = lp => lp is KeithEg
                },
                Proportionality = new Proportionality<ITermCategory>(ExtensionMethods.Defendant)
                {
                    GetChoice = lp => new NondeadlyForce()
                }
            };

            testResult = testSubject.IsValid(new KelseyEg(), new KeithEg());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
        }
    }

    public class KelseyEg : LegalPerson, IDefendant
    {
        public KelseyEg() : base("") { }
    }

    public class KeithEg : LegalPerson
    {
        public KeithEg() : base("") { }
    }
}
