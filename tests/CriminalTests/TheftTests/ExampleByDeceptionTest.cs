using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.AgainstProperty.Theft;
using NoFuture.Law.Criminal.US.Elements.AttendantCircumstances;
using NoFuture.Law.Criminal.US.Elements.Intent.PenalCode;
using NoFuture.Law.Property.US.FormsOf;
using NoFuture.Law.US.Persons;
using Xunit;

namespace NoFuture.Law.Criminal.Tests.TheftTests
{
    
    public class ExampleByDeceptionTest
    {
        [Fact]
        public void ExampleByFalseImpression()
        {
            var testCrime = new Misdemeanor
            {
                ActusReus = new ByDeception
                {
                    SubjectProperty = new ActOfService("TUNE-UP SERVICE"),
                    IsFalseImpression = lp => lp is JeremyTheifEg,
                    IsAcquiredTitle = lp => lp is JeremyTheifEg
                },
                MensRea = new Purposely
                {
                    IsKnowledgeOfWrongdoing = lp => lp is JeremyTheifEg,
                    IsIntentOnWrongdoing = lp => lp is JeremyTheifEg
                }
            };

            var testResult = testCrime.IsValid(new JeremyTheifEg());
            Console.WriteLine(testCrime.ToString());
            Assert.IsTrue(testResult);
        }

        [Fact]
        public void ExampleNotRelianceTest()
        {
            var testCrime = new Misdemeanor
            {
                ActusReus = new ByDeception
                {
                    SubjectProperty = new ActOfService("TUNE-UP SERVICE"),
                    IsFalseImpression = lp => lp is JeremyTheifEg,
                    IsAcquiredTitle = lp => lp is JeremyTheifEg
                },
                MensRea = new Purposely
                {
                    IsKnowledgeOfWrongdoing = lp => lp is JeremyTheifEg,
                    IsIntentOnWrongdoing = lp => lp is JeremyTheifEg
                }
            };
            //chuck was aware of the deception and attempted to use is for extortion
            var testReliance = new Reliance
            {
                IsReliantOnFalseRepresentation = lp => !(lp is ChuckUnrelianceEg)
            };

            testCrime.AttendantCircumstances.Add(testReliance);

            var testResult = testCrime.IsValid(new JeremyTheifEg(), new ChuckUnrelianceEg());
            Console.WriteLine(testCrime.ToString());
            Assert.IsFalse(testResult);
        }

        [Fact]
        public void ExampleRelianceTest()
        {
            var testCrime = new Misdemeanor
            {
                ActusReus = new ByDeception
                {
                    SubjectProperty = new ActOfService("TUNE-UP SERVICE"),
                    IsFalseImpression = lp => lp is JeremyTheifEg,
                    IsAcquiredTitle = lp => lp is JeremyTheifEg
                },
                MensRea = new Purposely
                {
                    IsKnowledgeOfWrongdoing = lp => lp is JeremyTheifEg,
                    IsIntentOnWrongdoing = lp => lp is JeremyTheifEg
                }
            };
            //chuck was aware of the deception and attempted to use is for extortion
            var testReliance = new Reliance
            {
                IsReliantOnFalseRepresentation = lp => lp is ChuckUnrelianceEg
            };

            testCrime.AttendantCircumstances.Add(testReliance);

            var testResult = testCrime.IsValid(new JeremyTheifEg(), new ChuckUnrelianceEg());
            Console.WriteLine(testCrime.ToString());
            Assert.IsTrue(testResult);
        }
    }

    public class ChuckUnrelianceEg : LegalPerson, IVictim, IDefendant
    {
        public ChuckUnrelianceEg() : base("CHUCK UNRELIANCE") { }
    }
}
