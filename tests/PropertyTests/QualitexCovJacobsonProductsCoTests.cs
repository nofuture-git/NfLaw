using System;
using NoFuture.Law.Property.US.FormsOf.Intellectus;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Property.Tests
{
    /// <summary>
    /// Qualitex Co. v. Jacobson Products Co., 514 U.S. 159 (1995)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, trademark is to distinguish product source
    /// ]]>
    /// </remarks>
    public class QualitexCovJacobsonProductsCoTests
    {
        private readonly ITestOutputHelper output;

        public QualitexCovJacobsonProductsCoTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void QualitexCovJacobsonProductsCo()
        {
            var test = new GreenGoldColoredCleaningPads();
            test.IsSecondaryMeaning = true;
            Assert.True(test.IsValid(new QualitexCo(), new JacobsonProductsCo()));
            this.output.WriteLine(test.ToString());
        }
    }

    public class GreenGoldColoredCleaningPads : Trademark
    {
        public GreenGoldColoredCleaningPads() : base("special shade of green-gold color on the pads") { }
        public Tuple<byte, byte, byte> RGB { get; } = new Tuple<byte, byte, byte>(151,153,72);
    }

    public class QualitexCo : LegalPerson, IPlaintiff
    {
        public QualitexCo(): base("Qualitex Co.") { }
    }

    public class JacobsonProductsCo : LegalPerson, IDefendant
    {
        public JacobsonProductsCo(): base("Jacobson Products Co.") { }
    }
}
