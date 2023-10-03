using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Defense;
using NoFuture.Law.Criminal.US.Defense.Justification;
using NoFuture.Law.Criminal.US.Elements;
using NoFuture.Law.Criminal.US.Elements.Act;
using NoFuture.Law.Criminal.US.Elements.Intent;
using NoFuture.Law.Criminal.US.Elements.Intent.PenalCode;
using NoFuture.Law.Criminal.US.Terms;
using NoFuture.Law.Criminal.US.Terms.Violence;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.DefenseTests.DefenseOfSelfTests
{
    /// <summary>
    /// Shuler v. Babbitt, 49 F.Supp.2d 1165 (1998)
    /// (this is a civil case)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, self-defense concerning a wild animal attack
    /// ]]>
    /// </remarks>
    public class ShulervBabbittTests
    {
        private readonly ITestOutputHelper output;

        public ShulervBabbittTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ShulervBabbitt()
        {
            var testCrime = new Infraction
            {
                ActusReus = new ActusReus
                {
                    IsVoluntary = lp => lp is Shuler,
                    IsAction = lp => lp is Shuler,
                },
                MensRea = StrictLiability.Value
            };

            var testResult = testCrime.IsValid(new Shuler());
            Assert.True(testResult);

            var testSubject = new DefenseOfSelf
            {
                IsReasonableFearOfInjuryOrDeath = lp => true,
                Imminence = new Imminence(ExtensionMethods.Defendant)
                {
                    GetResponseTime = lp => Imminence.NormalReactionTimeToDanger
                },
                Proportionality = new Proportionality<ITermCategory>(ExtensionMethods.Defendant)
                {
                    GetChoice = lp => new DeadlyForce()
                },
                Provocation = new Provocation(ExtensionMethods.Defendant)
                {
                    IsInitiatorOfAttack = lp => lp is GrizzlyBear,
                }
            };

            testResult = testSubject.IsValid(new Shuler(), new GrizzlyBear());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
        }
    }

    public class GrizzlyBear : LegalPerson, IVictim
    {
        
    }

    public class Shuler : LegalPerson, IDefendant
    {
        public Shuler() : base("JOHN E. SHULER") { }
    }

    public class Babbitt : LegalPerson
    {
        public Babbitt():base("BRUCE BABBITT, Secretary, Department of Interior") { }
    }
}
