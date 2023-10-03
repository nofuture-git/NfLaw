using System;
using NoFuture.Law.Procedure.Criminal.US;
using NoFuture.Law.Procedure.Criminal.US.SearchReasons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Procedure.Criminal.Tests
{
    public class ExampleAdministrativeCauseTests
    {
        private readonly ITestOutputHelper output;

        public ExampleAdministrativeCauseTests(ITestOutputHelper output)
        {
            this.output = output;
        }

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
            Assert.True(testResult);
            this.output.WriteLine(testSubject.ToString());
        }
    }

    public class ExampleAdminSearchPolicy : Rationale
    {
    }
}
