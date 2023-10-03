using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.Act;
using NoFuture.Law.Criminal.US.Elements.Homicide;
using NoFuture.Law.Criminal.US.Elements.Intent.PenalCode;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.HomicideTests
{
    public class ExampleHomicideInOtherTests
    {
        private readonly ITestOutputHelper output;

        public ExampleHomicideInOtherTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ExampleFelonyMurder()
        {
            var testFirstCrime = new Felony
            {
                ActusReus = new ActusReus
                {
                    IsVoluntary = lp => lp is JouquinBurnerEg,
                    IsAction = lp => lp is JouquinBurnerEg
                },
                MensRea = new Recklessly
                {
                    IsUnjustifiableRisk = lp => lp is JouquinBurnerEg,
                    IsDisregardOfRisk = lp => lp is JouquinBurnerEg
                }
            };

            var testResult = testFirstCrime.IsValid(new JouquinBurnerEg());
            this.output.WriteLine(testFirstCrime.ToString());
            Assert.True(testResult);

            var testCrime = new Felony
            {
                ActusReus = new HomicideInOther(testFirstCrime)
                {
                    IsCorpusDelicti = lp => lp is JouquinBurnerEg
                },
                MensRea = testFirstCrime.MensRea
            };

            testResult = testCrime.IsValid(new JouquinBurnerEg());
            this.output.WriteLine(testCrime.ToString());
            Assert.True(testResult);
        }

        [Fact]
        public void ExampleDeathOccursBefore()
        {
            var testFirstCrime = new Felony
            {
                ActusReus = new ActusReus
                {
                    IsVoluntary = lp => lp is JouquinBurnerEg,
                    IsAction = lp => lp is JouquinBurnerEg
                },
                MensRea = new Recklessly
                {
                    IsUnjustifiableRisk = lp => lp is JouquinBurnerEg,
                    IsDisregardOfRisk = lp => lp is JouquinBurnerEg
                }
            };

            var testResult = testFirstCrime.IsValid(new JouquinBurnerEg());
            this.output.WriteLine(testFirstCrime.ToString());
            Assert.True(testResult);
            var yyyy = DateTime.Today.Year;

            var testCrime = new Felony
            {
                ActusReus = new HomicideInOther(testFirstCrime)
                {
                    IsCorpusDelicti = lp => lp is JouquinBurnerEg,
                    Inception = new DateTime(yyyy, 3, 15, 9, 0, 0),
                    Terminus = new DateTime(yyyy, 3, 15, 9, 45, 0),
                    TimeOfTheDeath = new DateTime(yyyy, 3, 15, 10, 0, 0),
                },
                MensRea = testFirstCrime.MensRea
            };

            testResult = testCrime.IsValid(new JouquinBurnerEg());
            this.output.WriteLine(testCrime.ToString());
            Assert.False(testResult);
        }

        [Fact]
        public void ExampeMidemeanorManslaughter()
        {
            var testInputCrime = new Misdemeanor
            {
                ActusReus = new ActusReus
                {
                    IsVoluntary = lp => lp is RobertaBrandishEg,
                    IsAction = lp => lp is RobertaBrandishEg
                },
                MensRea = new Negligently
                {
                    IsUnjustifiableRisk = lp => lp is RobertaBrandishEg,
                    IsUnawareOfRisk = lp => lp is RobertaBrandishEg
                }
            };

            var testResult = testInputCrime.IsValid(new RobertaBrandishEg());
            this.output.WriteLine(testInputCrime.ToString());
            Assert.True(testResult);

            var testCrime = new Felony
            {
                ActusReus = new HomicideInOther(testInputCrime)
                {
                    IsCorpusDelicti = lp => lp is RobertaBrandishEg
                },
                MensRea = testInputCrime.MensRea
            };

            testResult = testCrime.IsValid(new RobertaBrandishEg());
            this.output.WriteLine(testCrime.ToString());
            Assert.True(testResult);
        }
    }

    public class JouquinBurnerEg : LegalPerson, IDefendant
    {
        public JouquinBurnerEg() : base("JOUQUIN BURNER") { }
    }

    public class RobertaBrandishEg : LegalPerson, IDefendant
    {
        public RobertaBrandishEg(): base("ROBERTA BRANDISH") { }
    }
}
