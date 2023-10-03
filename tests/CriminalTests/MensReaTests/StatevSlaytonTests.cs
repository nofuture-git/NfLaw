using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.Act;
using NoFuture.Law.Criminal.US.Elements.Intent;
using NoFuture.Law.Criminal.US.Elements.Intent.PenalCode;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.MensReaTests
{
    /// <summary>
    /// State v. Slayton, 154 P.3d 1057 (2007)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, strict liability 
    /// ]]>
    /// </remarks>
    public class StatevSlaytonTests
    {
        private readonly ITestOutputHelper output;

        public StatevSlaytonTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void StatevSlayton()
        {
            var testSubject = new Misdemeanor
            {
                ActusReus = new ActusReus
                {
                    IsAction = lp => lp is Slayton,
                    IsVoluntary = lp => lp is Slayton
                },
                MensRea = StrictLiability.Value
            };

            var testResult = testSubject.IsValid(new Slayton());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
        }
    }

    public class Slayton : LegalPerson, IDefendant
    {

    }
}
