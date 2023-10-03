using System;
using Xunit;
using NoFuture.Law.US.Persons;
using NoFuture.Law.Tort.US.Elements;
using NoFuture.Law.Tort.US.UnintentionalTort;
using NoFuture.Law.US;
using Xunit.Abstractions;

namespace NoFuture.Law.Tort.Tests
{
    /// <summary>
    /// Martin v. Herzog, 19 N.E.2d 987 (N.Y. 1920)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, failure to follow command of statue means negligence by its standard
    /// ]]>
    /// </remarks>
    public class MartinvHerzogTests
    {
        private readonly ITestOutputHelper output;

        public MartinvHerzogTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void MartinvHerzog()
        {
            var test = new NegligenceByStatute(ExtensionMethods.Tortfeasor)
            {
                IsObeyStatute = lp => !(lp is Herzog),
            };
            var testResult = test.IsValid(new Martin(), new Herzog());
            Assert.True(testResult);

            this.output.WriteLine(test.ToString());
        }
    }

    public class Martin : LegalPerson, IPlaintiff
    {
        public Martin(): base("Martin") { }
    }

    public class Herzog : LegalPerson, ITortfeasor
    {
        public Herzog(): base("Herzog") { }
    }
}
