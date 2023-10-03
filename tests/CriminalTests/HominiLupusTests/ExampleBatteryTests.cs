using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.AgainstPersons;
using NoFuture.Law.Criminal.US.Elements.AttendantCircumstances;
using NoFuture.Law.Criminal.US.Elements.Intent.ComLaw;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.HominiLupusTests
{
    public class ExampleBatteryTests
    {
        private readonly ITestOutputHelper output;

        public ExampleBatteryTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ExampleBattery()
        {
            var testCrime = new Misdemeanor
            {
                ActusReus = new Battery
                {
                    IsByViolence = lp => lp is HarrietIncestEg
                },
                MensRea = new GeneralIntent
                {
                    IsIntentOnWrongdoing = lp => lp is HarrietIncestEg
                }
            };

            var testResult = testCrime.IsValid(new HarrietIncestEg());
            this.output.WriteLine(testCrime.ToString());
            Assert.True(testResult);
        }

        [Fact]
        public void ExampleMutualCombat()
        {
            var testCrime = new Misdemeanor
            {
                ActusReus = new Battery
                {
                    IsByViolence = lp => lp is Combatent00,
                    Consent = new VictimConsent
                    {
                        IsCapableThereof = lp => true,
                        IsApprovalExpressed = lp => true,
                    }
                },
                MensRea = new GeneralIntent
                {
                    IsIntentOnWrongdoing = lp => lp is Combatent00
                },
            };
            var testResult = testCrime.IsValid(new Combatent00(), new Combatent01());
            this.output.WriteLine(testCrime.ToString());
            Assert.False(testResult);
        }
    }

    public class Combatent00 : LegalPerson, IDefendant
    {
        public Combatent00() : base("COMBATENT 00") { }
    }
    public class Combatent01 : LegalPerson, IVictim
    {
        public Combatent01() : base("COMBATENT 01") { }
    }
}
