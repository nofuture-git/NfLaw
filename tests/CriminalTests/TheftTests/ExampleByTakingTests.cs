using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.AgainstProperty.Theft;
using NoFuture.Law.Criminal.US.Elements.AttendantCircumstances;
using NoFuture.Law.Criminal.US.Elements.Intent.PenalCode;
using NoFuture.Law.Property.US.FormsOf;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.TheftTests
{
    public class ExampleByTakingTests
    {
        private readonly ITestOutputHelper output;

        public ExampleByTakingTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ExampleFiveFingerTheft()
        {
            var testCrime = new Misdemeanor
            {
                ActusReus = new ByTaking
                {
                    SubjectProperty = new ChewingGum(){ PropertyValue = dt => 1.25m },
                    IsTakenPossession = lp => lp is JeremyTheifEg,
                    IsAsportation = lp => lp is JeremyTheifEg
                },
                MensRea = new Purposely
                {
                    IsKnowledgeOfWrongdoing = lp => lp is JeremyTheifEg
                }
            };
            var testResult = testCrime.IsValid(new JeremyTheifEg());
            this.output.WriteLine(testCrime.ToString());
            Assert.True(testResult);
        }

        [Fact]
        public void ExampleEmbezzlementTheft()
        {
            var testCrime = new Misdemeanor
            {
                ActusReus = new ByTaking()
                {
                    SubjectProperty =  new TangiblePersonalProperty("payment for gas"),
                    IsTakenPossession = lp => lp is JeremyTheifEg,
                    IsAsportation = lp => lp is JeremyTheifEg
                },
                MensRea = new Purposely
                {
                    IsKnowledgeOfWrongdoing = lp => lp is JeremyTheifEg
                }
            };
            var testResult = testCrime.IsValid(new JeremyTheifEg());
            this.output.WriteLine(testCrime.ToString());
            Assert.True(testResult);
        }

        [Fact]
        public void ExampleInvalidTheftWhenOwner()
        {
            var jermey = new JeremyTheifEg();
            var property = new ChewingGum {IsEntitledTo = lp => lp.IsSamePerson(jermey), PropertyValue =  dt => 1.25m };
            var testCrime = new Misdemeanor
            {
                ActusReus = new ByTaking()
                {
                    SubjectProperty = property,
                    IsTakenPossession = lp => lp is JeremyTheifEg,
                    IsAsportation = lp => lp is JeremyTheifEg
                },
                MensRea = new Purposely
                {
                    IsKnowledgeOfWrongdoing = lp => lp is JeremyTheifEg
                }
            };
            var testResult = testCrime.IsValid(new JeremyTheifEg());
            this.output.WriteLine(testCrime.ToString());
            Assert.False(testResult);
        }

        [Fact]
        public void ExampleVictimConsentGiven()
        {
            var cody = new CodyFriendEg();
            var property = new ChewingGum {IsEntitledTo = lp => lp.IsSamePerson(cody)};

            var testCrime = new Misdemeanor
            {
                ActusReus = new ByTaking()
                {
                    SubjectProperty = property,
                    IsTakenPossession = lp => lp is JeremyTheifEg,
                    IsAsportation = lp => lp is JeremyTheifEg,
                    Consent = new VictimConsent
                    {
                        IsCapableThereof = lp => true,
                        //Cody said it was ok to take the property 
                        IsApprovalExpressed = lp => lp is CodyFriendEg,
                    }
                },
                MensRea = new Purposely
                {
                    IsKnowledgeOfWrongdoing = lp => lp is JeremyTheifEg
                },
            };
            var testResult = testCrime.IsValid(new JeremyTheifEg(), new CodyFriendEg());
            this.output.WriteLine(testCrime.ToString());
            Assert.False(testResult);
        }

        [Fact]
        public void ExampleTreacheryTest()
        {
            var testAttendantCircumstance = new FiduciaryRelationship
            {
                IsTrustBetween = (lp1, lp2) => (lp1 is JeremyTheifEg && lp2 is CodyFriendEg)
                                               || (lp1 is CodyFriendEg && lp2 is JeremyTheifEg)
            };

            var testCrime = new Misdemeanor
            {
                ActusReus = new ByTaking()
                {
                    SubjectProperty = new ChewingGum(){ PropertyValue = dt => 1.25m },
                    IsTakenPossession = lp => lp is JeremyTheifEg,
                    IsAsportation = lp => lp is JeremyTheifEg
                },
                MensRea = new Purposely
                {
                    IsKnowledgeOfWrongdoing = lp => lp is JeremyTheifEg
                }
            };
            testCrime.AttendantCircumstances.Add(testAttendantCircumstance);
            var testResult = testCrime.IsValid(new JeremyTheifEg(), new CodyFriendEg());
            this.output.WriteLine(testCrime.ToString());
            Assert.True(testResult);
        }
    }

    public class ChewingGum : LegalProperty
    {

    }

    public class JeremyTheifEg : LegalPerson, IDefendant
    {
        public JeremyTheifEg() : base("JEREMY THEIF") {}
    }

    public class CodyFriendEg : LegalPerson, IVictim
    {
        public CodyFriendEg() : base("CODY FRIEND") { }
    }
}
