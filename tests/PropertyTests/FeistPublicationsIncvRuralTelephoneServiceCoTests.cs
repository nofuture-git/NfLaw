using System;
using NoFuture.Law.Property.US.FormsOf.Intellectus;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Property.Tests
{
    /// <summary>
    /// Feist Publications, Inc. v. Rural Telephone Service Co., 499 U.S. 340 (1991)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, copyright requires creativity and originality, not a reward for artistic labour
    /// ]]>
    /// </remarks>
    public class FeistPublicationsIncvRuralTelephoneServiceCoTests
    {
        private readonly ITestOutputHelper output;

        public FeistPublicationsIncvRuralTelephoneServiceCoTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void FeistPublicationsIncvRuralTelephoneServiceCo()
        {
            var test = new TelephoneDirectory()
            {
                IsMinimalCreative = false,
                IsOriginalExpression = true,
            };

            var testResult = test.IsValid(new FeistPublicationsInc(), new RuralTelephoneServiceCo());
            Assert.False(testResult);
            this.output.WriteLine(test.ToString());
        }
    }

    public class TelephoneDirectory : Copyright
    {

    }

    public class FeistPublicationsInc : LegalPerson, IPlaintiff
    {
        public FeistPublicationsInc(): base("Feist Publications, Inc.") { }
    }

    public class RuralTelephoneServiceCo : LegalPerson, IDefendant
    {
        public RuralTelephoneServiceCo(): base("Rural Telephone Service Co.") { }
    }
}
