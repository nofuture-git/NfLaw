using System;
using NoFuture.Law;
using NoFuture.Law.Procedure.Civil.US.Jurisdiction;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Procedure.Civil.Tests
{
    public class ExampleTestFederalSubjectMatterJurisdiction
    {
        private readonly ITestOutputHelper output;

        public ExampleTestFederalSubjectMatterJurisdiction(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestSubjectMatterJurisdictionIsValid()
        {
            var testSubject = new FederalSubjectMatterJurisdiction(new SomeOtherKindOfCourt("kangaroo"))
            {
                IsAuthorized2ExerciseJurisdiction = lc => lc is SomeFederalLegalMatter,
                IsArisingFromFederalLaw = lc => lc is SomeFederalLegalMatter,
                GetAssertion = lp => new SomeFederalLegalMatter()
            };
            var testResult = testSubject.IsValid();
            this.output.WriteLine(testSubject.ToString());
            Assert.False(testResult);
        }
    }

    public class SomeFederalLegalMatter : LegalConcept
    {
        public override bool IsValid(params ILegalPerson[] persons)
        {
            return true;
        }
    }

    public class SomeOtherKindOfCourt : VocaBase, ICourt
    {
        public SomeOtherKindOfCourt(string name) : base(name) { }
    }
}
