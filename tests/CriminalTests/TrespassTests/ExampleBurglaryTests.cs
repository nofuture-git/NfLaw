using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.AgainstProperty.Trespass;
using NoFuture.Law.Criminal.US.Elements.AttendantCircumstances;
using NoFuture.Law.Criminal.US.Elements.Intent;
using NoFuture.Law.Criminal.US.Elements.Intent.ComLaw;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.TrespassTests
{
    public class ExampleBurglaryTests
    {
        private readonly ITestOutputHelper output;

        public ExampleBurglaryTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ExampleBurglaryActTest()
        {
            var testAct = new Burglary
            {
                //removing a simple barrier and placing hands beyond threshold is sufficient
                IsBreakingForce = lp => lp is JedBurglarToolEg,
                IsTangibleEntry = lp => lp is JedBurglarToolEg,
                IsStructuredEnclosure = lp => lp is SomeonesApartment,
                SubjectProperty = new SomeonesApartment()
            };

            var testResult = testAct.IsValid(new JedBurglarToolEg());
            this.output.WriteLine(testAct.ToString());
            Assert.True(testResult);

            testAct = new Burglary
            {
                IsTangibleEntry = lp => lp is JedBurglarToolEg,
                IsStructuredEnclosure = lp => lp is SomeonesApartment,
                SubjectProperty = new SomeonesApartment()
            };

            testResult = testAct.IsValid(new JedBurglarToolEg());
            this.output.WriteLine(testAct.ToString());
            Assert.True(testResult);

            testAct = new Burglary
            {
                IsBreakingForce = lp => lp is JedBurglarToolEg,
                IsTangibleEntry = lp => lp is JedBurglarToolEg,
                IsStructuredEnclosure = lp => lp is SomeonesApartment,
                SubjectProperty = new SomeonesApartment(),
                Consent = new VictimConsent
                {
                    IsApprovalExpressed = lp => false,
                    IsCapableThereof = lp => true,
                }
            };

            testResult = testAct.IsValid(new JedBurglarToolEg());
            this.output.WriteLine(testAct.ToString());
            Assert.True(testResult);

        }

        [Fact]
        public void TestBurglaryIntent()
        {
            var testCrime = new Misdemeanor
            {
                ActusReus = new Burglary
                {
                    IsBreakingForce = lp => lp is ChristianShovesEg,
                    IsTangibleEntry = lp => lp is ChristianShovesEg,
                    IsStructuredEnclosure = lp => lp is SupposedHauntedHouse
                },
                MensRea = new IsHouseHaunted()
            };
            var testResult = testCrime.IsValid(new ChristianShovesEg());
            this.output.WriteLine(testCrime.ToString());
            Assert.False(testResult);

            testCrime = new Misdemeanor
            {
                ActusReus = new CriminalTrespass
                {
                    IsTangibleEntry = lp => lp is ChristianShovesEg,
                },
                MensRea = new GeneralIntent
                {
                    IsKnowledgeOfWrongdoing = lp => lp is ChristianShovesEg
                }
            };

            testResult = testCrime.IsValid(new ChristianShovesEg());
            this.output.WriteLine(testCrime.ToString());
            Assert.True(testResult);
        }
    }

    public class SomeonesApartment : LegalProperty
    {

    }

    public class SupposedHauntedHouse : LegalProperty
    {

    }

    public class IsHouseHaunted : MensRea
    {
        public override bool IsValid(params ILegalPerson[] persons)
        {
            return true;
        }

        public override int CompareTo(object obj)
        {
            return 1;
        }
    }

    public class JedBurglarToolEg : LegalPerson, IDefendant
    {
        public JedBurglarToolEg() : base("JED BURGLARTOOL") {  }
    }

    public class HansDaresEg : LegalPerson
    {
        public HansDaresEg(): base("HANS DARES") {  }
    }

    public class ChristianShovesEg : LegalPerson, IDefendant
    {
        public ChristianShovesEg() : base("CHRISTIAN SHOVES") {  }
    }
}
