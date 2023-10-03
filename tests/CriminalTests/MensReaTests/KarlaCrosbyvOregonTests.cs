using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.Act;
using NoFuture.Law.Criminal.US.Elements.Intent.PenalCode;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.MensReaTests
{
    /// <summary>
    /// 154 P.3d 97 (2007) 342 Or. 419 STATE of Oregon, Respondent on Review, v. Karla CROSBY, Petitioner on Review. (CC0112-38549; CA A120319; SC S53295). Supreme Court of Oregon.
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// 
    /// ]]>
    /// </remarks>
    public class KarlaCrosbyvOregonTests
    {
        private readonly ITestOutputHelper output;

        public KarlaCrosbyvOregonTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void KarlaCrosbyvOregon()
        {
            var recklessly = new Recklessly
            {
                IsUnjustifiableRisk = lp => lp is KarlaCrosby,
                IsDisregardOfRisk = lp => lp is KarlaCrosby,
            };
            var testSubject = new Felony
            {
                ActusReus = new ActusReus
                {
                    IsAction = lp => lp is KarlaCrosby,
                    IsVoluntary = lp => lp is KarlaCrosby
                },
                MensRea = recklessly
            };

            var testResult = testSubject.IsValid(new KarlaCrosby());
            Assert.True(testResult);

            //the oregon supreme court found that the wording given to the jury made this false
            recklessly.IsDisregardOfRisk = lp => false;
            testResult = testSubject.IsValid(new KarlaCrosby());
            this.output.WriteLine(testSubject.ToString());
            Assert.False(testResult);
        }
    }

    public class KarlaCrosby : LegalPerson, IDefendant
    {
        public KarlaCrosby() : base("KARLA CROSBY") { }
    }
}
