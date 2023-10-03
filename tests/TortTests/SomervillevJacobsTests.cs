using System;
using Xunit;
using NoFuture.Law.US;
using NoFuture.Law.Tort.US.Elements;
using NoFuture.Law.US.Persons;
using Xunit.Abstractions;

namespace NoFuture.Law.Tort.Tests
{
    /// <summary>
    /// Somerville v. Jacobs, 170 S.E.2d 805 (W. Va. 1969)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, when both parties have acted in good faith court of equity will just make one pay the other to bring balance
    /// ]]>
    /// </remarks>
    public class SomervillevJacobsTests
    {
        private readonly ITestOutputHelper output;

        public SomervillevJacobsTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void SomervillevJacobs()
        {
            var test = new ImprovingTrespassers
            {
                SubjectProperty = new LegalProperty("Lot 47") {IsEntitledTo = lp => lp is Somerville},
                Consent = Consent.NotGiven(),
                IsOwnerGoodFaithOblivious = lp => lp is Somerville,
                IsTrespasserGoodFaithIntent = lp => lp is Jacobs
            };

            var testResult = test.IsValid(new Somerville(), new Jacobs());
            Assert.True(testResult);
            this.output.WriteLine(test.ToString());
        }
    }

    public class Somerville : LegalPerson, IPlaintiff
    {
        public Somerville(): base("Somerville") { }
    }

    public class Jacobs : LegalPerson, ITortfeasor
    {
        public Jacobs(): base("Jacobs") { }
    }
}
