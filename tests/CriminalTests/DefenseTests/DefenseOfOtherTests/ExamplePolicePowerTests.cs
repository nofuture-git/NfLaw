using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Defense;
using NoFuture.Law.Criminal.US.Elements.Act;
using NoFuture.Law.Criminal.US.Elements.Intent.ComLaw;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.DefenseTests.DefenseOfOtherTests
{
    public class ExamplePolicePowerTests
    {
        private readonly ITestOutputHelper output;

        public ExamplePolicePowerTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ExamplePolicePower()
        {
            var testCrime = new Felony
            {
                ActusReus = new ActusReus
                {
                    IsVoluntary = lp => lp is OfficerColinEg,
                    IsAction = lp => lp is OfficerColinEg
                },
                MensRea = new GeneralIntent
                {
                    IsKnowledgeOfWrongdoing = lp => lp is OfficerColinEg,
                    IsIntentOnWrongdoing = lp => lp is OfficerColinEg
                }
            };
            var testResult = testCrime.IsValid(new OfficerColinEg());
            Assert.True(testResult);

            var testSubject = new PolicePower
            {
                IsAgentOfTheState = lp => lp is OfficerColinEg,
                //example has officer shooting out window drive-by stlye on a fleeing person
                IsReasonableUseOfForce = lp => false
            };

            testResult = testSubject.IsValid(new OfficerColinEg(), new LindaEg());
            this.output.WriteLine(testSubject.ToString());
            Assert.False(testResult);
        }
    }

    public class OfficerColinEg : LegalPerson, IDefendant
    {
        public OfficerColinEg() : base("OFFICER COLIN") { }
    }

    public class LindaEg : LegalPerson, IVictim
    {
        public LindaEg() : base("LINDA BRATHEIF") {}
    }
}
