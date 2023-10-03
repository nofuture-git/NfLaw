using System;
using System.Collections.Generic;
using System.Linq;
using NoFuture.Law;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Procedure.Criminal.Tests
{
    public class ExampleAffidavitTests
    {
        private readonly ITestOutputHelper output;

        public ExampleAffidavitTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestAffidavitIsValid()
        {
            var testSubject = new Affidavit<object>
            {
                GetAffiant = lps => lps.FirstOrDefault(lp => lp is ExampleAffiant),
                GetWitness = lps => lps.FirstOrDefault(lp => lp is ExampleNotaryPublic),
                IsSigned = lp => lp is ExampleNotaryPublic || lp is ExampleAffiant,
                Attestation = new Tuple<IVoca, DateTime?>(new VocaBase("office"), DateTime.UtcNow),
                FactsThereof = new List<object> {new object()}
            };

            var testResult = testSubject.IsValid(new ExampleAffiant(), new ExampleNotaryPublic());
            Assert.True(testResult);
        }
    }

    public class ExampleAffiant : LegalPerson, IAffiant
    {
        public ExampleAffiant() : base("Sweary McSwearison") { }
    }

    public class ExampleNotaryPublic : LegalPerson, INotaryPublic
    {
        public ExampleNotaryPublic() : base("Signy McStampsalot") { }
    }
}
