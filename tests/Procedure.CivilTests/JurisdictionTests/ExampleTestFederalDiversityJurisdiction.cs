using System;
using NoFuture.Law;
using NoFuture.Law.Procedure.Civil.US.Jurisdiction;
using NoFuture.Law.US.Courts;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Procedure.Civil.Tests
{
    public class ExampleTestFederalDiversityJurisdiction
    {
        private readonly ITestOutputHelper output;

        public ExampleTestFederalDiversityJurisdiction(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestFederalDiversityJurisdictionIsValid()
        {
            var testSubject = new FederalDiversityJurisdiction(new FederalCourt("some district"))
            {
                GetDomicileLocation = lp =>
                {
                    if (lp is IPlaintiff)
                        return new VocaBase("Ohio");
                    if (lp is IDefendant)
                        return new VocaBase("Missouri");
                    return null;
                },
                GetInjuryClaimDollars = lp => 75000.01M
                
            };

            var testResult = testSubject.IsValid(new ExamplePlaintiff(), new ExampleDefendant());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
            testSubject.ClearReasons();

            testSubject.Court = new StateCourt("Ohio");
            testResult = testSubject.IsValid(new ExamplePlaintiff(), new ExampleDefendant());
            this.output.WriteLine(testSubject.ToString());
            Assert.False(testResult);
            testSubject.ClearReasons();

            testSubject.Court = new FederalCourt("some district");
            testSubject.GetInjuryClaimDollars = lp => 74999.99M;
            testResult = testSubject.IsValid(new ExamplePlaintiff(), new ExampleDefendant());
            this.output.WriteLine(testSubject.ToString());
            Assert.False(testResult);
            testSubject.ClearReasons();

            testSubject.GetDomicileLocation = lp =>
            {
                if (lp is IPlaintiff)
                    return new VocaBase("Missouri");
                if (lp is IDefendant)
                    return new VocaBase("Missouri");
                return null;
            };
            testResult = testSubject.IsValid(new ExamplePlaintiff(), new ExampleDefendant());
            this.output.WriteLine(testSubject.ToString());
            Assert.False(testResult);
            testSubject.ClearReasons();

            testResult = testSubject.IsValid(new ExamplePlaintiff(), new ExampleForeigner());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);

        }
    }

    public class ExampleForeigner : LegalPerson, IForeigner, IDefendant
    {
        public ExampleForeigner() : base("Fritz C Shlaphindagen") { }
    }
}
