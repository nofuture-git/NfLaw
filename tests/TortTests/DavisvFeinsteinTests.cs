using System;
using Xunit;
using NoFuture.Law.US.Persons;
using NoFuture.Law.Tort.US.ReasonableCare;
using NoFuture.Law.US;
using Xunit.Abstractions;

namespace NoFuture.Law.Tort.Tests
{
    /// <summary>
    /// Davis v. Feinstein, 88 A.2d 695 (Pa. 1952)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, afflicted are considered to have met the standard
    /// of care when using compensatory device. Not expected to
    /// actually meet the same nimble results of the unafflicted - only to try.
    /// ]]>
    /// </remarks>
    public class DavisvFeinsteinTests
    {
        private readonly ITestOutputHelper output;

        public DavisvFeinsteinTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void DavisvFeinstein()
        {
            var test = new OfPhysicalDeficiency(ExtensionMethods.Plaintiff)
            {
                IsAfflictedWith = lp => lp is Davis,
                IsUsingCompensatoryDevice = lp => lp is Davis
            };

            var testResult = test.IsValid(new Davis(), new Feinstein());
            Assert.True(testResult);

            this.output.WriteLine(test.ToString());
        }
    }

    public class Davis : LegalPerson, IPlaintiff
    {
        public Davis(): base("Davis") { }
    }

    public class Feinstein : LegalPerson, ITortfeasor
    {
        public Feinstein(): base("Feinstein") { }
    }
}
