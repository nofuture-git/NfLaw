using System;
using System.Collections.Generic;
using NoFuture.Law.Procedure.Criminal.US.Challenges;
using NoFuture.Law.Procedure.Criminal.US.SearchReasons;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Procedure.Criminal.Tests
{
    public class ExampleChallengeToProbableCauseTests
    {
        private readonly ITestOutputHelper output;

        public ExampleChallengeToProbableCauseTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestChallengeToProbableCauseIsValid00()
        {
            var testSubject = new ChallengeToProbableCause<string>
            {
                Affidavit = new ExampleAffidavit(),
                IsDisregardOfTruth = af => true,
                IsContainFalseStatement = af => af.FactsThereof.Contains("threw a hammer"),
                IsFalseStatementCritical = af => true
            };

            var testResult = testSubject.IsValid(new ExampleBadCop(), new ExampleNotaryPublic());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
        }
    }

    public class ExampleBadCop : ExampleLawEnforcement, IAffiant { }

    public class ExampleAffidavit : Affidavit<string>
    {
        public ExampleAffidavit()
        {
            FactsThereof = new List<string>
            {
                "threw a hammer"
            };
        }

        public override bool IsValid(params ILegalPerson[] persons)
        {
            return true;
        }
    }
}
