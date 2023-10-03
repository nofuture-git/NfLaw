using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Defense.Excuse;
using NoFuture.Law.Criminal.US.Elements.Act;
using NoFuture.Law.Criminal.US.Elements.Intent.ComLaw;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.DefenseTests
{
    /// <summary>
    /// People v. Register, 60 N.Y.2d 270 (1983)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, the intent was recklessness and voluntary intoxication brings such things to an end
    /// ]]>
    /// </remarks>
    public class PeoplevRegisterTests
    {
        private readonly ITestOutputHelper output;

        public PeoplevRegisterTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void PeoplevRegister()
        {
            var testCrime = new Felony
            {
                ActusReus = new ActusReus
                {
                    IsAction = lp => lp is Register,
                    IsVoluntary = lp => lp is Register,
                },
                MensRea = new GeneralIntent
                {
                    IsKnowledgeOfWrongdoing = lp => lp is Register,
                }
            };

            var testResult = testCrime.IsValid(new Register());
            Assert.True(testResult);

            var testSubject = new Intoxication
            {
                IsIntoxicated = lp => lp is Register,
                IsInvoluntary = lp => !(lp is Register),
            };

            testResult = testSubject.IsValid(new Register());
            this.output.WriteLine(testSubject.ToString());
            Assert.False(testResult);
        }
    }

    public class Register : LegalPerson, IDefendant
    {
        public Register() : base("BRUCE REGISTER") { }
    }
}
