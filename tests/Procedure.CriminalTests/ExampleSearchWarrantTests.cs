using System;
using System.Linq;
using NoFuture.Law;
using NoFuture.Law.Procedure.Criminal.US;
using NoFuture.Law.Procedure.Criminal.US.Warrants;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Procedure.Criminal.Tests
{
    public class ExampleSearchWarrantTests
    {
        private readonly ITestOutputHelper output;

        public ExampleSearchWarrantTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestSearchWarrantIsValid00()
        {
            var testSubject = new SearchWarrant
            {
                GetObjectiveOfSearch = () => new VocaBase("Billy's Closest on 123 Elm St"),
                IsObjectiveDescribedWithParticularity = r =>
                    string.Equals(r.Name, "Billy's Closest on 123 Elm St", StringComparison.OrdinalIgnoreCase),
                GetIssuerOfWarrant = lps => lps.FirstOrDefault(lp => lp is ExampleJudge),
                IsIssuerCapableDetermineProbableCause = lp => lp is ExampleJudge,
                IsIssuerNeutralAndDetached = lp => lp is ExampleJudge,
                ProbableCause = new ExampleProbableCause()
            };

            var testResult = testSubject.IsValid(new ExampleSuspect(), new ExampleLawEnforcement(), new ExampleJudge());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
        }
    }

    public class ExampleProbableCause : LegalConcept
    {
        public override bool IsValid(params ILegalPerson[] persons)
        {
            return true;
        }
    }

    public class ExampleJudge : LegalPerson, IJudge
    {
        public ExampleJudge():base("E.B. Judgin") { }
    }
}
