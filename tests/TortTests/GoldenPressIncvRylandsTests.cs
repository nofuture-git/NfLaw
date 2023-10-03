using System;
using NoFuture.Law.Tort.US.Elements;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Tort.Tests
{
    /// <summary>
    /// Golden Press, Inc. v. Rylands, 235 P.2d 592 (Colo. 1951).
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, encroachment can be ignored is some slight circumstances
    /// ]]>
    /// </remarks>
    public class GoldenPressIncvRylandsTests
    {
        private readonly ITestOutputHelper output;

        public GoldenPressIncvRylandsTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void GoldenPressIncvRylands()
        {
            var test = new Encroachment
            {
                Consent = Consent.NotGiven(),
                SubjectProperty = new LegalProperty("some building")
                {
                    IsEntitledTo = lp => lp is Rylands
                },
                IsGoodFaithIntent = lp => true,
                IsUseAffected = lp => false,
                IsRemovalCostGreat = lp => lp is Rylands
            };
            var testResult = test.IsValid(new GoldenPressInc(), new Rylands());
            Assert.False(testResult);
            this.output.WriteLine(test.ToString());
        }
    }

    public class GoldenPressInc : LegalPerson, IPlaintiff
    {
        public GoldenPressInc(): base("Golden Press, Inc.") { }
    }

    public class Rylands : LegalPerson, IDefendant
    {
        public Rylands(): base("Rylands") { }
    }
}
