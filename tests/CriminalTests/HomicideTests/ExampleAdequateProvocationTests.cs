﻿using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.Homicide;
using NoFuture.Law.Criminal.US.Elements.Intent.ComLaw;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.HomicideTests
{
    public class ExampleAdequateProvocationTests
    {
        private readonly ITestOutputHelper output;

        public ExampleAdequateProvocationTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ExampleIndequateProvocation()
        {
            var testCrime = new Felony
            {
                ActusReus = new ManslaughterVoluntary
                {
                    IsCorpusDelicti = DillonRagerEg.IsKilledFrank
                },
                MensRea = new AdequateProvocation
                {
                    //getting fired is not reason enough
                    IsReasonableToInciteKilling = lp => false,
                    IsDefendantActuallyProvoked = lp => lp is DillonRagerEg,
                    IsVictimSourceOfIncite = lp => lp is DillonRagerEg
                }
            };

            var testResult = testCrime.IsValid(new DillonRagerEg());
            this.output.WriteLine(testCrime.ToString());
            Assert.False(testResult);
        }

        [Fact]
        public void ExampleAdequateProvation()
        {
            var testCrime = new Felony
            {
                ActusReus = new ManslaughterVoluntary
                {
                    IsCorpusDelicti = JoseRagerEg.IsKilledWife
                },
                MensRea = new AdequateProvocation
                {
                    IsReasonableToInciteKilling = lp => lp is JoseRagerEg,
                    IsDefendantActuallyProvoked = lp => lp is JoseRagerEg,
                    IsVictimSourceOfIncite = lp => lp is JoseRagerEg
                }
            };

            var testResult = testCrime.IsValid(new JoseRagerEg());
            this.output.WriteLine(testCrime.ToString());
            Assert.True(testResult);
        }

        [Fact]
        public void DoesntWorkWithOtherIntent()
        {
            var testCrime = new Felony
            {
                ActusReus = new ManslaughterVoluntary
                {
                    IsCorpusDelicti = JoseRagerEg.IsKilledWife
                },
                MensRea = new SpecificIntent
                {
                    IsIntentOnWrongdoing = lp => lp is JoseRagerEg
                }
            };

            var testResult = testCrime.IsValid(new JoseRagerEg());
            this.output.WriteLine(testCrime.ToString());
            Assert.False(testResult);
        }

        [Fact]
        public void TestBadTiming()
        {
            var yyyy = DateTime.Today.Year;
            var testCrime = new Felony
            {
                ActusReus = new ManslaughterVoluntary
                {
                    IsCorpusDelicti = JoseRagerEg.IsKilledWife
                },
                MensRea = new AdequateProvocation
                {
                    IsReasonableToInciteKilling = lp => lp is JoseRagerEg,
                    IsDefendantActuallyProvoked = lp => lp is JoseRagerEg,
                    IsVictimSourceOfIncite = lp => lp is JoseRagerEg,
                    Inception = new DateTime(yyyy, 3, 15, 14,0,0),
                    Terminus = new DateTime(yyyy, 3,15, 14,5,0),
                    //heat of passion doesn't work over time
                    TimeOfTheDeath = new DateTime(yyyy, 3, 16, 6, 0, 0),
                }
            };

            var testResult = testCrime.IsValid(new JoseRagerEg());
            this.output.WriteLine(testCrime.ToString());
            Assert.False(testResult);
        }
    }

    public class DillonRagerEg : LegalPerson, IDefendant
    {
        public DillonRagerEg() : base("DILLON RAGER") { }

        public static bool IsKilledFrank(ILegalPerson lp)
        {
            return lp is DillonRagerEg;
        }
    }

    public class JoseRagerEg : LegalPerson, IDefendant
    {
        public JoseRagerEg() : base("JOSE RAGER") { }

        public static bool IsKilledWife(ILegalPerson lp)
        {
            return lp is JoseRagerEg;
        }
    }
}
