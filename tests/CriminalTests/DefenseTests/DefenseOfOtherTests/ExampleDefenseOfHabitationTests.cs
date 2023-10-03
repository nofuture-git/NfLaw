using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Defense.Justification;
using NoFuture.Law.Criminal.US.Elements.Act;
using NoFuture.Law.Criminal.US.Elements.Intent.ComLaw;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.DefenseTests.DefenseOfOtherTests
{
    public class ExampleDefenseOfHabitationTests
    {
        private readonly ITestOutputHelper output;

        public ExampleDefenseOfHabitationTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ExampleDefenseOfHabitation()
        {
            var testCrime = new Felony
            {
                ActusReus = new ActusReus
                {
                    IsAction = lp => lp is BobEg,
                    IsVoluntary = lp => lp is BobEg
                },
                MensRea = new GeneralIntent
                {
                    IsKnowledgeOfWrongdoing = lp => lp is BobEg,
                    IsIntentOnWrongdoing = lp => lp is BobEg
                }
            };
            var testResult = testCrime.IsValid(new BobEg());
            Assert.True(testResult);

            var testSubject = new DefenseOfHabitation
            {
                IsIntruderEnterResidence = lp => true,
                IsOccupiedResidence = lp => true,
            };

            testResult = testSubject.IsValid(new NateEg(), new NateEg());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
        }
    }

    public class NateEg : LegalPerson, IDefendant
    {
        public NateEg():base("NATE EXAMPLE") {}
    }

    public class BobEg : LegalPerson, IDefendant
    {
        public BobEg() : base("BOB LOVERBOY") {}
    }
}
