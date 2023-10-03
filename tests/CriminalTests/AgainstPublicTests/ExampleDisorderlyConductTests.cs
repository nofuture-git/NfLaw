using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.AgainstPublic;
using NoFuture.Law.Criminal.US.Elements.Intent.PenalCode;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.AgainstPublicTests
{
    public class ExampleDisorderlyConductTests
    {
        private readonly ITestOutputHelper output;

        public ExampleDisorderlyConductTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ExampleDirorderlyConductAct()
        {
            var testAct = new DisorderlyConduct
            {
                IsUnreasonablyLoud = lp => lp is DavidInebriatedEg || lp is DanielDrunkbuddyEg,
            };

            var testResult = testAct.IsValid(new DavidInebriatedEg(), new DanielDrunkbuddyEg());
            this.output.WriteLine(testAct.ToString());
            Assert.True(testResult);
        }

        [Fact]
        public void TestWithAttendantCircumstances()
        {
            var publicPlace = new PublicPlace("sidewalk")
                {IsWithin = lp => lp is DavidInebriatedEg || lp is DanielDrunkbuddyEg};
            var testCrime = new Misdemeanor
            {
                ActusReus = new DisorderlyConduct
                {
                    IsUnreasonablyLoud = lp => lp is DavidInebriatedEg || lp is DanielDrunkbuddyEg
                },
                MensRea = new Recklessly
                {
                    IsDisregardOfRisk = lp => lp is DavidInebriatedEg || lp is DanielDrunkbuddyEg,
                    IsUnjustifiableRisk = lp => lp is DavidInebriatedEg || lp is DanielDrunkbuddyEg
                },
                AttendantCircumstances = { publicPlace }
            };
            var testResult = testCrime.IsValid(new DavidInebriatedEg(), new DanielDrunkbuddyEg());
            this.output.WriteLine(testCrime.ToString());
            Assert.True(testResult);

            publicPlace.IsAccessibleToPublic = false;
            publicPlace.Name = "private residence in the country";
            testResult = testCrime.IsValid(new DavidInebriatedEg(), new DanielDrunkbuddyEg());
            this.output.WriteLine(testCrime.ToString());
            Assert.False(testResult);
        }
    }

    public class DavidInebriatedEg : LegalPerson, IDefendant
    {
        public DavidInebriatedEg() : base("DAVID INEBRIATED") { }
    }

    public class DanielDrunkbuddyEg : LegalPerson, IDefendant
    {
        public DanielDrunkbuddyEg() : base("DANIEL DRUNKBUDDY") { }
    }

}
