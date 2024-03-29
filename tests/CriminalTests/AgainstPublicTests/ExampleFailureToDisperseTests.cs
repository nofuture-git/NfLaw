﻿using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.AgainstPublic;
using NoFuture.Law.Criminal.US.Elements.Intent.PenalCode;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.AgainstPublicTests
{
    public class ExampleFailureToDisperseTests
    {
        private readonly ITestOutputHelper output;

        public ExampleFailureToDisperseTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ExampleUnlawfullAssembly()
        {
            var testCrime = new Misdemeanor
            {
                ActusReus = new UnlawfulAssembly
                {
                    IsUnreasonablyLoud = lp => lp is BuckShouterEg,
                    IsCombative = lp => lp is BuckShouterEg,
                    IsGroupMember = lp => lp is BuckShouterEg
                },
                MensRea = new Purposely
                {
                    IsIntentOnWrongdoing = lp => lp is BuckShouterEg
                }
            };
            var testResult = testCrime.IsValid(new BuckShouterEg(), new BuckBudoneEg(), new BuckBudtwoEg(),
                new BuckBudthreeEg(), new BuckBudfourEg(), new BuckBudfiveEg());
            this.output.WriteLine(testCrime.ToString());
            Assert.True(testResult);
        }

        [Fact]
        public void ExampleOrderToDisperse()
        {
            var testCrime = new Misdemeanor
            {
                ActusReus = new FailureToDisperse
                {
                    IsUnreasonablyLoud = lp => lp is BuckShouterEg,
                    IsCombative = lp => lp is BuckShouterEg,
                    IsGroupMember = lp => lp is BuckShouterEg,
                    IsOrderedToDisperse = lp => lp is BuckShouterEg
                },
                MensRea = new Purposely
                {
                    IsIntentOnWrongdoing = lp => lp is BuckShouterEg
                }
            };
            var testResult = testCrime.IsValid(new BuckShouterEg(), new BuckBudoneEg(), new BuckBudtwoEg(),
                new BuckBudthreeEg(), new BuckBudfourEg(), new BuckBudfiveEg());
            this.output.WriteLine(testCrime.ToString());
            Assert.True(testResult);
        }

        [Fact]
        public void ExampleRiotTest()
        {
            var testCrime = new Felony
            {
                ActusReus = new Riot
                {
                    IsUnreasonablyLoud = lp => lp is BuckShouterEg,
                    IsCombative = lp => lp is BuckShouterEg,
                    IsGroupMember = lp => lp is BuckShouterEg,
                    IsByViolence = lp => lp is BuckShouterEg
                },
                MensRea = new Purposely
                {
                    IsIntentOnWrongdoing = lp => lp is BuckShouterEg
                }
            };
            var testResult = testCrime.IsValid(new BuckShouterEg(), new BuckBudoneEg(), new BuckBudtwoEg(),
                new BuckBudthreeEg(), new BuckBudfourEg(), new BuckBudfiveEg());
            this.output.WriteLine(testCrime.ToString());
            Assert.True(testResult);
        }
    }

    public class BuckShouterEg : LegalPerson, IDefendant
    {
        public BuckShouterEg(): base("BUCK SHOUTER") { }
    }

    public class BuckBudoneEg : BuckShouterEg { }
    public class BuckBudtwoEg : BuckShouterEg { }
    public class BuckBudthreeEg : BuckShouterEg { }
    public class BuckBudfourEg : BuckShouterEg { }
    public class BuckBudfiveEg : BuckShouterEg { }

}
