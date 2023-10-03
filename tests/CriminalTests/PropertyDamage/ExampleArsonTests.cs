using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.AgainstProperty.Damage;
using NoFuture.Law.Criminal.US.Elements.Intent.ComLaw;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.PropertyDestruction
{
    public class ExampleArsonTests
    {
        private readonly ITestOutputHelper output;

        public ExampleArsonTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestArsonAct()
        {
            var testAct = new Arson
            {
                SubjectProperty = new LegalProperty("patch of grass"),
                IsBurned = lp => lp?.Name == "patch of grass",
                IsFireStarter = lp => lp is ClarkBoredburnEg || lp is MannyBoredtwobrunEg
            };
            var testResult = testAct.IsValid(new ClarkBoredburnEg(), new MannyBoredtwobrunEg());
            this.output.WriteLine(testAct.ToString());
            Assert.True(testResult);
        }

        [Fact]
        public void TestArsonAsOwner()
        {
            var testCrime = new Felony
            {
                ActusReus = new Arson
                {
                    SubjectProperty = new LegalProperty("ex's stuff")
                    {
                        IsEntitledTo = lp => lp is TimBrokenheartEg
                    },
                    IsBurned = lp => lp?.Name == "ex's stuff",
                    IsFireStarter = lp => lp is TimBrokenheartEg
                },
                MensRea = new GeneralIntent
                {
                    IsKnowledgeOfWrongdoing = lp => lp is TimBrokenheartEg
                }
            };

            var testResult = testCrime.IsValid(new TimBrokenheartEg());
            this.output.WriteLine(testCrime.ToString());
            Assert.False(testResult);
        }
    }

    public class ClarkBoredburnEg : LegalPerson, IDefendant
    {
        public ClarkBoredburnEg() : base("CLARK BOREDBURN") { }
    }

    public class MannyBoredtwobrunEg : LegalPerson, IDefendant
    {
        public MannyBoredtwobrunEg() : base("MANNY BOREDTWOBRUN") { }
    }

    public class TimBrokenheartEg : LegalPerson, IDefendant
    {
        public TimBrokenheartEg() : base("TIM BROKENHEART") { }
    }
}
