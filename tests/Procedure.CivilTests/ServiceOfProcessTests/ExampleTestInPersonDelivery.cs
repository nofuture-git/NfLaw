using System;
using NoFuture.Law.Procedure.Civil.US.ServiceOfProcess;
using NoFuture.Law.US.Courts;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Procedure.Civil.Tests.ServiceOfProcessTests
{
    public class ExampleTestInPersonDelivery
    {
        private readonly ITestOutputHelper output;

        public ExampleTestInPersonDelivery(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestInPersonDeliveryIsValid()
        {
            var testSubject = new InPersonDelivery
            {
                Court = new StateCourt("UT"),
                GetDeliveredTo = lp => lp is ICourtOfficial ? new ExampleDefendant() : null,
                GetToDateOfService = lp => DateTime.Today.AddDays(-14),
                IsToDeliverAuthorized = lp => lp is ILawEnforcement,
                IsToReceiveAuthorized = lp => true
            };

            var testResult = testSubject.IsValid(new ExamplePlaintiff(), new ExampleDefendant(),
                new ExampleLawEnforcement());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
        }
    }

    public class ExampleLawEnforcement : LegalPerson, ILawEnforcement
    {
        public ExampleLawEnforcement() :base("Johnny Law") { }
    }
}
