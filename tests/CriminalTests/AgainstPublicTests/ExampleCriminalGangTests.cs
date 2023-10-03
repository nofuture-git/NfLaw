using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.Act;
using NoFuture.Law.Criminal.US.Terms;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.AgainstPublicTests
{
    public class ExampleCriminalGangTests
    {
        private readonly ITestOutputHelper output;

        public ExampleCriminalGangTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestIsCriminalGang()
        {
            var testGang = new CriminalGang("North Side Boys");
            for (var i = 0; i < 55; i++)
            {
                testGang.Add(new NorthSideBoyEg());
            }
            testGang.Idiosyncrasies.Add(new GangColors());
            testGang.Idiosyncrasies.Add(new GangTatoo());
            testGang.CriminalActivity.Add(new DrugDealin());

            var testResult = testGang.IsValid(new MikeWannbeEg());
            testGang.Add(new MikeWannbeEg());
            this.output.WriteLine(testGang.ToString());
            Assert.True(testResult);

        }

        [Fact]
        public void TestIsGangMember()
        {
            var testGang = new CriminalGang("North Side Boys");
            for (var i = 0; i < 55; i++)
            {
                testGang.Add(new NorthSideBoyEg());
            }
            testGang.Idiosyncrasies.Add(new GangColors());
            testGang.Idiosyncrasies.Add(new GangTatoo());
            testGang.CriminalActivity.Add(new DrugDealin());
            testGang.Add(new MikeWannbeEg());

            Assert.True(testGang.IsGangMember(new MikeWannbeEg()));
        }
    }

    public class GangColors : Idiosyncrasy
    {
        protected override string CategoryName => "gang colors: black and white";
    }

    public class GangTatoo : Idiosyncrasy
    {
        protected override string CategoryName => "gang tatoo";
    }

    public class NorthSideBoyEg : LegalPerson
    {
        public NorthSideBoyEg() : base("NORTH SIDE BOY") { }
    }

    public class MikeWannbeEg : LegalPerson, IDefendant
    {
        public MikeWannbeEg() : base("MIKE WANNABE") { }
    }

    public class DrugDealin : ActusReus
    {

    }
}
