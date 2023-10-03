using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Defense;
using NoFuture.Law.Criminal.US.Elements.Act;
using NoFuture.Law.Criminal.US.Elements.Intent;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.DefenseTests.DefenseOfOtherTests
{
    /// <summary>
    /// Dutton v. Hayes-Pupko, No. 03-06-00438 (2008).
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, the use of police power must be reasonable
    /// ]]>
    /// </remarks>
    public class DuttonvHayesPupkoTests
    {
        private readonly ITestOutputHelper output;

        public DuttonvHayesPupkoTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void DuttonvHayesPupko()
        {
            var testSue = new Misdemeanor
            {
                ActusReus = new ActusReus
                {
                    IsAction = lp => true,
                    IsVoluntary = lp => true
                },
                MensRea = StrictLiability.Value
            };

            var testResult = testSue.IsValid(new Dutton());
            Assert.True(testResult);

            var testSubject = new PolicePower
            {
                IsAgentOfTheState = lp => lp is Dutton,
                IsReasonableUseOfForce = lp => false
            };

            testResult = testSubject.IsValid(new Dutton(), new HayesPupko());
            this.output.WriteLine(testSubject.ToString());
            Assert.False(testResult);
        }
    }

    public class Dutton : LegalPerson, IDefendant
    {
        public Dutton() : base("DERRICK DUTTON") { }
    }

    public class HayesPupko : LegalPerson, IVictim
    {
        public HayesPupko() : base("SHERYL HAYES-PUPKO") {}
    }
}
