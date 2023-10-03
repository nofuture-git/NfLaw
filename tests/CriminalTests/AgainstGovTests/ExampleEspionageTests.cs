using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.AgainstGov;
using NoFuture.Law.Criminal.US.Elements.Intent.ComLaw;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.AgainstGovTests
{
    public class ExampleEspionageTests
    {
        private readonly ITestOutputHelper output;

        public ExampleEspionageTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestEspionage()
        {
            var testCrime = new Felony
            {
                ActusReus = new Espionage("nuclear secrets")
                {
                    IsTransmitor = lp => lp is EthelRosenberg || lp is JuliusRosenberg,
                },
                MensRea = new GeneralIntent
                {
                    IsKnowledgeOfWrongdoing = lp => lp is EthelRosenberg || lp is JuliusRosenberg
                }
            };

            var testResult = testCrime.IsValid(new EthelRosenberg(), new JuliusRosenberg());
            this.output.WriteLine(testCrime.ToString());
            Assert.True(testResult);
        }
    }

    public class EthelRosenberg : LegalPerson, IDefendant
    {
        public EthelRosenberg(): base ("ETHEL ROSENBERG") {  }
    }

    public class JuliusRosenberg : LegalPerson, IDefendant
    {
        public JuliusRosenberg():base("JULIUS ROSENBERG") { }
    }

}
