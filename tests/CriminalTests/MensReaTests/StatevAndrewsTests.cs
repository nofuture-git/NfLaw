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
    /// State v. Andrews, 572 S.E.2d 798 (2002)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, transferred intent is anyone else hurt by the intent and suffices as the intent element of the crime charged
    /// ]]>
    /// </remarks>
    public class StatevAndrewsTests
    {
        private readonly ITestOutputHelper output;

        public StatevAndrewsTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void StatevAndrews()
        {
            var testIntent = new Knowingly
            {
                IsKnowledgeOfWrongdoing = lp => lp is Andrews,
                IsIntentOnWrongdoing = lp => lp is Andrews
            };

            var testSubject = new Felony
            {
                ActusReus = new ActusReus
                {
                    IsVoluntary = lp => lp is Andrews,
                    IsAction = lp => lp is Andrews
                },
                MensRea = new Transferred(testIntent)
            };

            var testResult = testSubject.IsValid(new Andrews());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
        }
    }

    public class Andrews : LegalPerson, IDefendant
    {

    }
}
