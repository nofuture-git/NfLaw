using System;
using NoFuture.Law.Procedure.Civil.US.Pleadings;
using NoFuture.Law.US.Courts;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Procedure.Civil.Tests
{
    public class ExampleTestInterveneJoiner
    {
        private readonly ITestOutputHelper output;

        public ExampleTestInterveneJoiner(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestInterveneJoinerIsValid()
        {
            var testSubject = new InterveneJoiner
            {
                GetAssertion = lp => new ExampleCauseForAction(),
                Court = new StateCourt("WY"),
                IsStatueAuthorizedRight = lp => lp is ExampleAbsentee
            };

            var testResult =
                testSubject.IsValid(new ExamplePlaintiff(), new ExampleDefendant(), new ExampleThirdParty());
            this.output.WriteLine(testSubject.ToString());
            Assert.False(testResult);
            testSubject.ClearReasons();

            testResult =
                testSubject.IsValid(new ExamplePlaintiff(), new ExampleDefendant(), new ExampleAbsentee());
            Assert.True(testResult);

            testSubject.IsFeasible = lp => false;
            testSubject.IsStatueAuthorizedRight = lp => false;
            testResult =
                testSubject.IsValid(new ExamplePlaintiff(), new ExampleDefendant(), new ExampleAbsentee{IsIndispensable = true});
            this.output.WriteLine(testSubject.ToString());
            Assert.False(testResult);
            testSubject.IsFeasible = lp => true;
            testSubject.IsStatueAuthorizedRight = lp => lp is ExampleAbsentee;

        }
    }

    public class ExampleAbsentee : LegalPerson, IAbsentee
    {
        public ExampleAbsentee() : base("Absent Mr T") { }

        public bool IsIndispensable { get; set; }
    }
}
