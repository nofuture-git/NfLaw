using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Defense.Excuse;
using NoFuture.Law.Criminal.US.Elements.Act;
using NoFuture.Law.Criminal.US.Elements.Intent;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.DefenseTests
{
    /// <summary>
    /// Garnett v. State, 632 A.2d 797 (1993)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, statutory rape is a strict liability crime
    /// ]]>
    /// </remarks>
    public class GarnettvStateTests
    {
        private readonly ITestOutputHelper output;

        public GarnettvStateTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void GarnettvState()
        {
            var testCrime = new Felony
            {
                ActusReus = new ActusReus
                {
                    IsVoluntary = lp => lp is Garnett,
                    IsAction = lp => lp is Garnett
                },
                MensRea = StrictLiability.Value
            };

            var testResult = testCrime.IsValid(new Garnett());
            Assert.True(testResult);

            var testSubject = new MistakeOfFact
            {
                IsBeliefNegateIntent = lp => lp is Garnett,
                IsStrictLiability = testCrime.MensRea is StrictLiability
            };

            testResult = testSubject.IsValid(new Garnett());
            this.output.WriteLine(testSubject.ToString());
            Assert.False(testResult);
            
        }
    }

    public class Garnett : LegalPerson, IDefendant
    {
        public Garnett() : base("LENNARD GARNETT") { }
    }
}
