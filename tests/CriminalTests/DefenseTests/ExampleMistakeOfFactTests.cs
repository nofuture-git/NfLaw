using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Defense.Excuse;
using NoFuture.Law.Criminal.US.Elements.Act;
using NoFuture.Law.Criminal.US.Elements.Intent;
using NoFuture.Law.Criminal.US.Elements.Intent.ComLaw;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.DefenseTests
{
    public class ExampleMistakeOfFactTests
    {
        private readonly ITestOutputHelper output;

        public ExampleMistakeOfFactTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ExampleMistakeOfFactCorrect()
        {
            var testCrime = new Misdemeanor
            {
                ActusReus = new ActusReus
                {
                    IsVoluntary = lp => lp is MickieEg,
                    IsAction = lp => lp is MickieEg,
                },
                MensRea = new GeneralIntent
                {
                    //this is the point, prosecution says this, mistake-of-fact undo's it
                    IsKnowledgeOfWrongdoing = lp => lp is MickieEg
                }
            };

            var testResult = testCrime.IsValid(new MickieEg());
            Assert.True(testResult);

            var testSubject = new MistakeOfFact
            {
                IsBeliefNegateIntent = lp => lp is MickieEg,
                IsStrictLiability = testCrime.MensRea is StrictLiability
            };

            testResult = testSubject.IsValid(new MickieEg());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
        }

        [Fact]
        public void ExampleMistakeOfFactIncorrect()
        {
            var testCrime = new Misdemeanor
            {
                ActusReus = new ActusReus
                {
                    IsVoluntary = lp => lp is TinaEg,
                    IsAction = lp => lp is TinaEg,
                },
                MensRea = StrictLiability.Value
            };

            var testResult = testCrime.IsValid(new TinaEg());
            Assert.True(testResult);

            var testSubject = new MistakeOfFact
            {
                IsBeliefNegateIntent = lp => lp is TinaEg,
                IsStrictLiability = testCrime.MensRea is StrictLiability
            };

            testResult = testSubject.IsValid(new TinaEg());
            this.output.WriteLine(testSubject.ToString());
            Assert.False(testResult);
        }
    }

    public class TinaEg : LegalPerson, IDefendant
    {
        public TinaEg() : base("TINA SPEEDER") {}
    }

    public class MickieEg : LegalPerson, IDefendant
    {
        public MickieEg() : base("MICKIE BIKE") {}
    }
}
