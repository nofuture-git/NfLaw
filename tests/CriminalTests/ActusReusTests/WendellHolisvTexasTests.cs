using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements;
using NoFuture.Law.Criminal.US.Elements.Act;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.ActusReusTests
{
    /// <summary>
    /// 998 S.W.2d 363 (1999) Wendell Hollis OLER, Appellant, v. The STATE of Texas, Appellee. No. 05-97-01229-CR. Court of Appeals of Texas, Dallas.
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, possession is another kind of actus reus 
    /// ]]>
    /// </remarks>
    public class WendellHolisvTexasTests
    {
        private readonly ITestOutputHelper output;

        public WendellHolisvTexasTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void WendllHolisvTexas()
        {
            var testSubject = new Felony();
            //court contents the actus reus is by possession, therefore omission is not applicable
            var testPossession = new Possession();
            testSubject.ActusReus = testPossession;
            testPossession.IsKnowinglyProcured = lp => lp is WendellHollis;
            var testResult = testPossession.IsValid(new WendellHollis());
            this.output.WriteLine(testPossession.ToString());
            Assert.True(testResult);

            //defendant contents no statute duty to report multiple prescriptions
            var testOmission = new Duty {IsStatuteOrigin = lp => false};
            testResult = testOmission.IsValid(new WendellHollis());
            Assert.False(testResult);
        }
    }

    public class WendellHollis : LegalPerson, IDefendant
    {
        public WendellHollis() : base("WENDELL HOLLIS") { }
    }
}
