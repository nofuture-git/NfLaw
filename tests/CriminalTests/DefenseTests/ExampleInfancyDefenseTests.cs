using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Defense.Excuse;
using NoFuture.Law.Criminal.US.Elements.Act;
using NoFuture.Law.Criminal.US.Elements.Intent.ComLaw;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.DefenseTests
{
    public class ExampleInfancyDefenseTests
    {
        private readonly ITestOutputHelper output;

        public ExampleInfancyDefenseTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ExampleInfancyDefense()
        {
            var testCrime = new Felony
            {
                ActusReus = new ActusReus
                {
                    IsAction = lp => lp is MarioEg,
                    IsVoluntary = lp => lp is MarioEg,
                },
                MensRea = new GeneralIntent
                {
                    IsKnowledgeOfWrongdoing = lp => lp is MarioEg,
                }
            };

            var testResult = testCrime.IsValid(new MarioEg());
            Assert.True(testResult);

            var testSubject = new Infancy();

            testResult = testSubject.IsValid(new MarioEg());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
        }
    }

    public class MarioEg : LegalPerson, IDefendant, IChild
    {
        public MarioEg() : base("MARIO CANDY") { }
    }
}
