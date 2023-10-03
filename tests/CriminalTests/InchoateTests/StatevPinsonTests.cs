using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.Inchoate;
using NoFuture.Law.Criminal.US.Elements.Intent.ComLaw;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.InchoateTests
{
    /// <summary>
    /// State v. Pinson, 895 P.2d 274 (1995)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, two-person crime (buy\sell) if each side has some 
    /// particular name, the charge of solicitation must match the defendant's role in the crime
    /// ]]>
    /// </remarks>
    public class StatevPinsonTests
    {
        private readonly ITestOutputHelper output;

        public StatevPinsonTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void StatevPinson()
        {
            var testCrime = new Felony
            {
                ActusReus = new Solicitation
                {
                    //Pinson was a buyer and trafficking is for selling
                    IsInduceAnotherToCrime = Pinson.IsSolicitationToTraffic
                },
                MensRea = new SpecificIntent
                {
                    IsKnowledgeOfWrongdoing = lp => lp is Pinson
                }
            };

            var testResult = testCrime.IsValid(new Pinson());
            this.output.WriteLine(testCrime.ToString());
            Assert.False(testResult);
        }
    }

    public class Pinson : LegalPerson, IDefendant
    {
        public Pinson() : base("JOHNNY PINSON") { }
        public bool IsBuyer { get; set; } = true;

        public static bool IsSolicitationToTraffic(ILegalPerson lp)
        {
            var pinson = lp as Pinson;
            if (pinson == null)
                return false;

            return !pinson.IsBuyer;
        }
    }
}
