using System;
using NoFuture.Law.Procedure.Civil.US.Pleadings;
using NoFuture.Law.Procedure.Civil.US.ServiceOfProcess;
using NoFuture.Law.US;
using NoFuture.Law.US.Courts;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Procedure.Civil.Tests.PleadingsTests
{
    public class ExampleTestAmendment
    {
        private readonly ITestOutputHelper output;

        public ExampleTestAmendment(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestAmendmentIsValid()
        {
            var testSubject = new Amendment()
            {
                Court = new StateCourt("CA"),
                GetServiceOfProcess = lp => new VoluntaryEntry { GetToDateOfService = lp1 => DateTime.UtcNow.AddDays(-14) },
                LinkedTo = new Complaint(),
            };

            var testResult = testSubject.IsValid(new ExamplePlaintiff(), new ExampleDefendant());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);


            testSubject = new Amendment()
            {
                Court = new StateCourt("CA"),
                GetServiceOfProcess = lp => new VoluntaryEntry { GetToDateOfService = lp1 => DateTime.UtcNow.AddDays(-45) },
                LinkedTo = new Complaint(),
            };

            testResult = testSubject.IsValid(new ExamplePlaintiff(), new ExampleDefendant());
            this.output.WriteLine(testSubject.ToString());
            Assert.False(testResult);
        }

        [Fact]
        public void TestAmendmentIsValidWithLeave()
        {
            var testSubject = new Amendment()
            {
                Court = new StateCourt("CA"),
                GetServiceOfProcess = lp => new VoluntaryEntry { GetToDateOfService = lp1 => DateTime.UtcNow.AddDays(-45) },
                LinkedTo = new Complaint(),
                //since its linked to a complaint (and not an answer), the opposition is the defense
                Assent =  new Consent { IsApprovalExpressed = lp => lp is IDefendant}
            };

            var testResult = testSubject.IsValid(new ExamplePlaintiff(), new ExampleDefendant());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
        }
    }
}
