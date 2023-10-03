using System;
using NoFuture.Law.Criminal.US.Elements.AgainstProperty.Damage;
using NoFuture.Law.Criminal.US.Elements.AttendantCircumstances;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.PropertyDestruction
{
    public class ExampleCrimMischiefTests
    {
        private readonly ITestOutputHelper output;

        public ExampleCrimMischiefTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestCriminalMischiefAct()
        {
            var testAct = new CriminalMischief
            {
                Consent = new VictimConsent
                {
                    IsApprovalExpressed = lp => false,
                    IsCapableThereof = lp => lp is SueBrokenstuffEg
                },
                IsCauseOfDamage = lp => lp is JohnnyDestroyerEg,
                IsDamaged = prop => prop?.Name == "sue's stuff",
                SubjectProperty = new LegalProperty("sue's stuff")
                {
                    IsInPossessionOf = lp => lp is SueBrokenstuffEg,
                    IsEntitledTo = lp => lp is SueBrokenstuffEg
                }
            };
            var testResult = testAct.IsValid(new JohnnyDestroyerEg(), new SueBrokenstuffEg());
            this.output.WriteLine(testAct.ToString());
            Assert.True(testResult);
        }
    }

    public class JohnnyDestroyerEg : LegalPerson, IDefendant
    {
        public JohnnyDestroyerEg() : base("JOHNNY DESTROYER") { }
    }

    public class SueBrokenstuffEg : LegalPerson, IVictim
    {
        public SueBrokenstuffEg(): base("SUE BROKENSTUFF") {  }
    }
}
