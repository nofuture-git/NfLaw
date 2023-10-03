using System;
using Xunit;
using NoFuture.Law.US;
using NoFuture.Law.Tort.US.IntentionalTort;
using NoFuture.Law.US.Persons;

namespace NoFuture.Law.Tort.Tests
{
    /// <summary>
    /// White v. Samsung Electronics America, Inc., 971 F.2d 1395 (9th Cir. 1992).
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// 
    /// ]]>
    /// </remarks>
    
    public class WhitevSamsungElectronicsAmericaIncTests
    {
        [Fact]
        public void WhitevSamsungElectronicsAmericaInc()
        {
            var test = new FalseEndorsement(ExtensionMethods.Defendant)
            {
                IsCommercialUse = lp => lp is SamsungElectronicsAmericaInc,
                IsAppropriatedPersonIdentity = (l1, l2) => l1 is White && l2 is SamsungElectronicsAmericaInc,
                IsFirstAmendmentProtected = lp => false,
            };

            var testResult = test.IsValid(new White(), new SamsungElectronicsAmericaInc());
            Assert.IsTrue(testResult);
            Console.WriteLine(test.ToString());
        }
    }

    public class White : LegalPerson, IPlaintiff
    {
        public White(): base("Vanna White") { }
    }

    public class SamsungElectronicsAmericaInc : LegalPerson, ITortfeasor
    {
        public SamsungElectronicsAmericaInc(): base("Samsung Electronics America, Inc.") { }
    }
}
