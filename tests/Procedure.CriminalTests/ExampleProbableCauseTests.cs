using System;
using System.Linq;
using NoFuture.Law.Procedure.Criminal.US;
using NoFuture.Law.Procedure.Criminal.US.SearchReasons;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Procedure.Criminal.Tests
{
    public class ExampleProbableCauseTests
    {
        private readonly ITestOutputHelper output;

        public ExampleProbableCauseTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestProbableCauseIsValid00()
        {
            var testSubject = new ProbableCause
            {
                IsFactsConcludeToCriminalActivity = lp => lp is ExampleLawEnforcement
            };

            var testResult = testSubject.IsValid(new ExampleSuspect(), new ExampleLawEnforcement());
            Assert.True(testResult);
            this.output.WriteLine(testSubject.ToString());
        }

        [Fact]
        public void TestProbableCauseIsValid01()
        {
            var testSubject = new ProbableCause
            {
                GetInformationSource = lps => lps.FirstOrDefault(lp => lp is IInformant),
                IsFactsConcludeToCriminalActivity = lp => lp is ExampleLawEnforcement
            };

            var testResult = testSubject.IsValid(
                new ExampleSuspect(),
                new ExampleInformant {IsInformationSufficientlyReliable = true, IsPersonSufficientlyCredible = true},
                new ExampleLawEnforcement());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
        }

        [Fact]
        public void TestProbableCauseIsValid02()
        {
            var testSubject = new ProbableCause
            {
                GetInformationSource = lps => lps.FirstOrDefault(lp => lp is IInformant),
                IsFactsConcludeToCriminalActivity = lp => lp is ExampleLawEnforcement
            };

            var testResult = testSubject.IsValid(
                new ExampleSuspect(),
                new ExampleInformant { IsInformationSufficientlyReliable = true, IsPersonSufficientlyCredible = false },
                new ExampleLawEnforcement());
            this.output.WriteLine(testSubject.ToString());
            Assert.False(testResult);
        }
    }

    public class ExampleSuspect : LegalPerson, ISuspect
    {
        public ExampleSuspect() :base("Shaddy McShadison") { }
    }

    public class ExampleInformant : LegalPerson, IInformant
    {
        public ExampleInformant():base("Info Tweeker") { }

        public bool IsPersonSufficientlyCredible { get; set; }
        public bool IsInformationSufficientlyReliable { get; set; }
    }
}
