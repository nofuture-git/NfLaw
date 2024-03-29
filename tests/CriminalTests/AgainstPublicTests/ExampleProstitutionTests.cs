﻿using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.AgainstPublic;
using NoFuture.Law.Criminal.US.Elements.Intent.PenalCode;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.AgainstPublicTests
{
    public class ExampleProstitutionTests
    {
        private readonly ITestOutputHelper output;

        public ExampleProstitutionTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestProstitution()
        {
            var testCrime = new Felony
            {
                ActusReus = new Prostitution
                {
                    Acceptance = sex => new LegalProperty("money") { PropertyValue = dt => 500m},
                    Assent = new Deal
                    {
                        IsApprovalExpressed = lp => lp is JohnPayerEg || lp is SueProstitueEg
                    },
                    Offer = new SexBilateral()
                    {
                        IsSexualIntercourse = lp => true,
                        IsOneOfTwo = lp => lp is JohnPayerEg || lp is SueProstitueEg
                    }
                },
                MensRea = new Purposely
                {
                    IsKnowledgeOfWrongdoing = lp => lp is JohnPayerEg || lp is SueProstitueEg
                }
            };
            var testResult = testCrime.IsValid(new SueProstitueEg());
            this.output.WriteLine(testCrime.ToString());
            Assert.True(testResult);

            testResult = testCrime.IsValid(new JohnPayerEg());
            this.output.WriteLine(testCrime.ToString());
            Assert.True(testResult);
        }

        [Fact]
        public void TestPimping()
        {
            var testCrime = new Felony
            {
                ActusReus = new Pimping
                {
                    IsKnowinglyReceived = lp => lp is UpgraddPimpEg || lp is SueProstitueEg,
                    IsFromProstitute = lp => lp is UpgraddPimpEg || lp is SueProstitueEg
                },
                MensRea = new Purposely
                {
                    IsKnowledgeOfWrongdoing = lp => lp is UpgraddPimpEg || lp is SueProstitueEg
                }
            };
            var testResult = testCrime.IsValid(new UpgraddPimpEg(), new SueProstitueEg());
            this.output.WriteLine(testCrime.ToString());
            Assert.True(testResult);
        }
    }

    public class JohnPayerEg : LegalPerson, IDefendant
    {
        public JohnPayerEg() : base("THE JOHN") {  }
    }

    public class SueProstitueEg : LegalPerson, IDefendant
    {
        public SueProstitueEg() : base("SUE PROSTITUE") {  }
    }

    public class UpgraddPimpEg : LegalPerson, IDefendant
    {
        public UpgraddPimpEg() : base("UPGRADD$") { }
    }
}
