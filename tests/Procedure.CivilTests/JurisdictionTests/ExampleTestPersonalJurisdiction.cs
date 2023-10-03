using System;
using NoFuture.Law;
using NoFuture.Law.Procedure.Civil.US.Jurisdiction;
using NoFuture.Law.US;
using NoFuture.Law.US.Courts;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Procedure.Civil.Tests
{
    public class ExampleTestPersonalJurisdiction
    {
        private readonly ITestOutputHelper output;

        public ExampleTestPersonalJurisdiction(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestPersonalJurisdictionIsValid()
        {
            var testSubject = new PersonalJurisdiction(new StateCourt("CA"))
            {
                GetDomicileLocation = lp => lp is ExampleDefendant ? new VocaBase("CA") : null,
            };

            var testResult = testSubject.IsValid(new ExamplePlaintiff(), new ExampleDefendant());
            Assert.True(testResult);
            this.output.WriteLine(testSubject.ToString());
        }

        [Fact]
        public void TestIsValidWithConsent()
        {
            const string MA = "Massachusetts";
            const string GA = "Georgia";

            var testSubject = new PersonalJurisdiction(new StateCourt(MA))
            {
                Consent = Consent.NotGiven() ,
                GetDomicileLocation = lp => lp is ExampleDefendant ? new VocaBase(GA) : new VocaBase(MA),
                GetCurrentLocation = lp => lp is ExampleDefendant ? new VocaBase(GA) : new VocaBase(MA),
                GetInjuryLocation = lp => lp is ExamplePlaintiff ? new VocaBase(MA) : null
            };

            var testResult = testSubject.IsValid(new ExamplePlaintiff(), new ExampleDefendant());
            Assert.False(testResult);
            this.output.WriteLine(testSubject.ToString());

            testSubject.Consent = Consent.IsGiven();
            testResult = testSubject.IsValid(new ExamplePlaintiff(), new ExampleDefendant());
            Assert.True(testResult);
            this.output.WriteLine(testSubject.ToString());
        }

        [Fact]
        public void TestBothLiveSameState()
        {
            const string MA = "Massachusetts";
            const string GA = "Georgia";

            var testSubject = new PersonalJurisdiction(new StateCourt(MA))
            {
                GetDomicileLocation = lp => new VocaBase(MA),
                GetInjuryLocation = lp => new VocaBase(GA)
            };

            var testResult = testSubject.IsValid(new ExamplePlaintiff(), new ExampleDefendant());
            Assert.True(testResult);
            this.output.WriteLine(testSubject.ToString());
        }
    }

    public class ExamplePlaintiff : LegalPerson, IPlaintiff
    {
        public ExamplePlaintiff() : base("Example P. Lantiff") { }
    }

    public class ExampleDefendant : LegalPerson, IDefendant
    {
        public ExampleDefendant() : base("Example D. Fendant") {  }
    }
}
