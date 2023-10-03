using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Defense.Excuse.Insanity;
using NoFuture.Law.Criminal.US.Elements.Act;
using NoFuture.Law.Criminal.US.Elements.Intent.ComLaw;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.DefenseTests.InsanityTests
{
    public class ExampleIrrImpulseTests
    {
        private readonly ITestOutputHelper output;

        public ExampleIrrImpulseTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ExampleIrrImpulseFake()
        {
            var testCrime = new Felony
            {
                ActusReus = new ActusReus
                {
                    IsAction = lp => lp is JoleneEg,
                    IsVoluntary = lp => lp is JoleneEg,
                },
                MensRea = new GeneralIntent
                {
                    IsIntentOnWrongdoing = lp => lp is JoleneEg,
                }
            };

            var testResult = testCrime.IsValid(new JoleneEg());
            Assert.True(testResult);

            var testSubject = new IrresistibleImpulse
            {
                IsMentalDefect = lp => lp is JoleneEg,
                //having not acted on in one case is obvious is volitional
                IsVolitional = lp => true
            };

            testResult = testSubject.IsValid(new JoleneEg());
            this.output.WriteLine(testSubject.ToString());
            Assert.False(testResult);
        }
    }

    public class JoleneEg : LegalPerson, IDefendant
    {
        public JoleneEg() : base("JOLENE CUTTER") {}
    }
}
