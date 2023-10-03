using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.Homicide;
using NoFuture.Law.Criminal.US.Elements.Intent;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.HomicideTests
{
    public class ExampleFirstDegreeTests
    {
        private readonly ITestOutputHelper output;

        public ExampleFirstDegreeTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ExampleFirstDegree()
        {
            var testCrime = new Felony
            {
                ActusReus = new MurderFirstDegree
                {
                    IsPremediated = JoanniePremedEg.IsPremeditated,
                    IsCorpusDelicti = lp => lp is JoanniePremedEg
                },
                MensRea = new DeadlyWeapon("handgun")
                {
                    IsUtilizable = lp => lp is JoanniePremedEg,
                    IsCanCauseDeath = lp => lp is JoanniePremedEg
                }
            };

            var testResult = testCrime.IsValid(new JoanniePremedEg());
            this.output.WriteLine(testCrime.ToString());
            Assert.True(testResult);
        }
    }

    public class JoanniePremedEg : LegalPerson, IDefendant
    {
        public JoanniePremedEg() : base("JOANNIE PREMED") { }

        public bool IsCalm { get; set; } = true;
        public bool IsMethodical { get; set; } = true;
        public bool IsManufacturedExcuse { get; set; } = true;
        public bool IsFakingActions { get; set; } = true;
        public bool IsMutlipleShots { get; set; } = true;

        public bool IsDeliberate => IsCalm && IsMethodical;
        public bool IsPlanned => IsManufacturedExcuse && IsFakingActions;
        public bool IsSpecificIntent => IsMutlipleShots;

        public static bool IsPremeditated(ILegalPerson lp)
        {
            var joannie = lp as JoanniePremedEg;
            if (joannie == null)
                return false;
            return joannie.IsDeliberate || joannie.IsPlanned || joannie.IsSpecificIntent;
        }
    }
}
