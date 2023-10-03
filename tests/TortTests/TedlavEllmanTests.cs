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
    /// Tedla v. Ellman, 19 N.E.2d 287 (N.Y. 1939)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, disobedience to a statue when doing so actually acheives its goal while obeying it does just the opposite
    /// ]]>
    /// </remarks>
    public class TedlavEllmanTests
    {
        private readonly ITestOutputHelper output;

        public TedlavEllmanTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TedlavEllman()
        {
            var test = new NegligenceByStatute(ExtensionMethods.Tortfeasor)
            {
                IsObeyStatute = lp => !(lp is Ellman),
                IsObedienceAddToDanger = lp => lp is Ellman
            };

            var testResult = test.IsValid(new Tedla(), new Ellman());
            Assert.False(testResult);

            this.output.WriteLine(test.ToString());
        }
    }

    public class Tedla : LegalPerson, IPlaintiff
    {
        public Tedla(): base("Tedla") { }
    }

    public class Ellman : LegalPerson, ITortfeasor
    {
        public Ellman(): base("Ellman") { }
    }
}
