using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.Inchoate;
using NoFuture.Law.Criminal.US.Elements.Intent.ComLaw;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.InchoateTests
{
    public class ExampleSolicitationTests
    {
        private readonly ITestOutputHelper output;

        public ExampleSolicitationTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ExampleSolicitation()
        {
            var testCrime = new Felony
            {
                ActusReus = new Solicitation
                {
                    IsInduceAnotherToCrime = lp => lp is JimmyRequestorEg
                },
                MensRea = new SpecificIntent
                {
                    IsKnowledgeOfWrongdoing = lp => lp is JimmyRequestorEg
                }
            };

            var testResult = testCrime.IsValid(new JimmyRequestorEg());
            this.output.WriteLine(testCrime.ToString());
            Assert.True(testResult);
        }
    }

    public class JimmyRequestorEg : LegalPerson, IDefendant
    {
        public JimmyRequestorEg() : base("JIMMY REQUESTOR") {}
    }
}
