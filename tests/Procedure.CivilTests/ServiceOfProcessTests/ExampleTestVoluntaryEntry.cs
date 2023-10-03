using System;
using NoFuture.Law.Procedure.Civil.US.ServiceOfProcess;
using NoFuture.Law.US.Courts;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Procedure.Civil.Tests.ServiceOfProcessTests
{
    public class ExampleTestVoluntaryEntry
    {
        private readonly ITestOutputHelper output;

        public ExampleTestVoluntaryEntry(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestVoluntaryEntryIsValid()
        {
            var testSubject = new VoluntaryEntry
            {
                Court = new StateCourt("WV"),
                IsSigned = lp => lp is IDefendant || lp is INotaryPublic,
                GetToDateOfService = lp => DateTime.Today.AddDays(-14),
            };

            var testResult =
                testSubject.IsValid(new ExamplePlaintiff(), new ExampleDefendant(), new ExampleNotaryPublic());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
        }
    }

    public class ExampleNotaryPublic : LegalPerson, INotaryPublic
    {
        public ExampleNotaryPublic() : base("Notia P. Ublic") { }
    }
}
