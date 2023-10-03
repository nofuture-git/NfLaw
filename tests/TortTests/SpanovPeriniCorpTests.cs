using System;
using Xunit;
using NoFuture.Law.US.Persons;
using NoFuture.Law.Tort.US.IntentionalTort;
using NoFuture.Law.US;
using Xunit.Abstractions;

namespace NoFuture.Law.Tort.Tests
{
    /// <summary>
    /// Spano v. Perini Corp., 250 N.E.2d 31 (N.Y. 1969)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, illustrate concept of abnormally dangerous activity
    /// ]]>
    /// </remarks>
    public class SpanovPeriniCorpTests
    {
        private readonly ITestOutputHelper output;

        public SpanovPeriniCorpTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void SpanovPeriniCorp()
        {
            var property = new LegalProperty("garage in Brooklyn");
            var test = new AbnormallyDangerousActivity(ExtensionMethods.Tortfeasor)
            {
                IsExplosives = p => p.Equals(property),
                SubjectProperty = property,
                Injury = new Damage(ExtensionMethods.Tortfeasor)
                {
                    SubjectProperty = property,
                    ToValue = p => true,
                },
            };

            var testResult = test.IsValid(new Spano(), new PeriniCorp());
            Assert.True(testResult);
            this.output.WriteLine(test.ToString());
        }
    }

    public class Spano : LegalPerson, IPlaintiff
    {
        public Spano(): base("Spano") { }
    }

    public class PeriniCorp : LegalPerson, ITortfeasor
    {
        public PeriniCorp(): base("Perini Corp.") { }
    }
}
