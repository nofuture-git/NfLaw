using System;
using NoFuture.Law.Procedure.Criminal.US.Interrogations;
using Xunit;

namespace NoFuture.Law.Procedure.Criminal.Tests
{
    
    public class ExampleVoluntarinessTests
    {
        [Fact]
        public void TestVoluntarinessIsValid00()
        {
            var testSubject = new Voluntariness
            {
                IsSubjectedToCoercivePoliceConduct = lp => lp is ExampleSuspect,
                IsSufficientToOvercomeWillpower = lp => lp is ExampleSuspect
            };

            var testResult = testSubject.IsValid(new ExampleLawEnforcement(), new ExampleSuspect());
            Console.WriteLine(testSubject.ToString());
            Assert.IsFalse(testResult);
        }
    }
}
