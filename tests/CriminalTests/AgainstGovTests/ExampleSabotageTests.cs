using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.AgainstGov;
using NoFuture.Law.Criminal.US.Elements.AttendantCircumstances;
using NoFuture.Law.Criminal.US.Elements.Intent.PenalCode;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.AgainstGovTests
{
    public class ExampleSabotageTests
    {
        private readonly ITestOutputHelper output;

        public ExampleSabotageTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestSabotage()
        {
            var testCrime = new Felony
            {
                ActusReus = new Sabotage("computer system")
                {
                    IsDamagerOf = lp => lp is MoSabotageEg,
                    IsDefenseProperty = true,
                    IsEntitledTo = lp => lp is UsDeptDefense,
                    IsInPossessionOf = lp => lp is UsDeptDefense
                },
                MensRea = new Purposely
                {
                    IsIntentOnWrongdoing = lp => lp is MoSabotageEg
                },
                AttendantCircumstances = { new NationalEmergency()}
            };

            var testResult = testCrime.IsValid(new MoSabotageEg());
            this.output.WriteLine(testCrime.ToString());
            Assert.True(testResult);
        }
    }

    public class UsDeptDefense : Government
    {
        public UsDeptDefense() : base("US Department of Defense") { }
       
    }

    public class MoSabotageEg : MoDisgruntledEg, IDefendant
    {
        public MoSabotageEg() : base("MO SABOTAGE") { }
    }
}
