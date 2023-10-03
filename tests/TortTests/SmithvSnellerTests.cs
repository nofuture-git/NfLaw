using System;
using Xunit;
using NoFuture.Law.US.Persons;
using NoFuture.Law.Tort.US.ReasonableCare;
using NoFuture.Law.US;
using Xunit.Abstractions;

namespace NoFuture.Law.Tort.Tests
{
    /// <summary>
    /// Smith v. Sneller, 26 A.2d 452 (Pa. 1942)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, its not a greater duty of physical disability to meet the rational care standard
    /// ]]>
    /// </remarks>
    public class SmithvSnellerTests
    {
        private readonly ITestOutputHelper output;

        public SmithvSnellerTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void SmithvSneller()
        {
            var test = new OfPhysicalDeficiency(ExtensionMethods.Plaintiff)
            {
                IsUsingCompensatoryDevice = lp => !(lp is Smith),
                IsAfflictedWith = lp => lp is Smith
            };

            var testResult = test.IsValid(new Smith(), new Sneller());
            Assert.False(testResult);
            this.output.WriteLine(test.ToString());
        }
    }

    public class Smith : LegalPerson, IPlaintiff
    {
        public Smith(): base("Smith") { }
    }

    public class Sneller : LegalPerson, ITortfeasor
    {
        public Sneller(): base("Sneller") { }
    }
}
