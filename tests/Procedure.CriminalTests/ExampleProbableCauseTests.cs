using System;
using System.Linq;
using NoFuture.Law.Procedure.Criminal.US;
using NoFuture.Law.Procedure.Criminal.US.SearchReasons;
using NoFuture.Law.US.Persons;
using Xunit;

namespace NoFuture.Law.Procedure.Criminal.Tests
{
    
    public class ExampleProbableCauseTests
    {
        [Fact]
        public void TestProbableCauseIsValid00()
        {
            var testSubject = new ProbableCause
            {
                IsFactsConcludeToCriminalActivity = lp => lp is ExampleLawEnforcement
            };

            var testResult = testSubject.IsValid(new ExampleSuspect(), new ExampleLawEnforcement());
            Assert.IsTrue(testResult);
            Console.WriteLine(testSubject.ToString());
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
            Console.WriteLine(testSubject.ToString());
            Assert.IsTrue(testResult);
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
            Console.WriteLine(testSubject.ToString());
            Assert.IsFalse(testResult);
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
