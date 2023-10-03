using System;
using Xunit;
using NoFuture.Law.US;
using NoFuture.Law.Tort.US.IntentionalTort;
using NoFuture.Law.US.Persons;
using Xunit.Abstractions;

namespace NoFuture.Law.Tort.Tests
{
    /// <summary>
    /// Midler v. Ford Motor Company, 849 F.2d 460 (9th. Cir. 1986).
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, personality identity need not be specific picture or name but enought to recognize
    /// ]]>
    /// </remarks>
    public class MidlervFordMotorCompanyTests
    {
        private readonly ITestOutputHelper output;

        public MidlervFordMotorCompanyTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void MidlervFordMotorCompany()
        {
            var test = new FalseEndorsement(ExtensionMethods.Defendant)
            {
                IsFirstAmendmentProtected = lp => false,
                IsCommercialUse = lp => lp is FordMotorCompany,
                IsAppropriatedPersonIdentity = (l1, l2) => l1 is Midler && l2 is FordMotorCompany
            };
            var testResult = test.IsValid(new Midler(), new FordMotorCompany());
            Assert.True(testResult);
            this.output.WriteLine(test.ToString());
        }
    }

    public class Midler : LegalPerson, IPlaintiff
    {
        public Midler(): base("Bette Midler") { }
    }

    public class FordMotorCompany : LegalPerson, ITortfeasor
    {
        public FordMotorCompany(): base("Ford Motor Company") { }
    }
}
