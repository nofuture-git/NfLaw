using System;
using NoFuture.Law.Procedure.Criminal.US;
using NoFuture.Law.Procedure.Criminal.US.SearchReasons;
using Xunit;

namespace NoFuture.Law.Procedure.Criminal.Tests
{
    
    public class ExampleAdministrativeCauseTests
    {
        [Fact]
        public void TestAdministrativeCauseIsValid00()
        {
            var testSubject = new AdministrativeCause<ExampleAdminSearchPolicy>
            {
                IsPolicyBased = t => t is ExampleAdminSearchPolicy,
                IsToDetectCriminalActivity = t => false,
                IsMinimumIntrusion = t => t is ExampleAdminSearchPolicy,
                IsSpecificDetection = t => t is ExampleAdminSearchPolicy
            };

            var testResult = testSubject.IsValid();
            Assert.IsTrue(testResult);
            Console.WriteLine(testSubject.ToString());
        }
    }

    public class ExampleAdminSearchPolicy : Rationale
    {
    }
}
