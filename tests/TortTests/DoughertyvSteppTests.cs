using System;
using NoFuture.Law.Property.US.FormsOf;
using NoFuture.Law.Property.US.FormsOf.InTerra;
using Xunit;
using NoFuture.Law.US.Persons;
using NoFuture.Law.Tort.US.IntentionalTort;
using NoFuture.Law.US;
using Xunit.Abstractions;

namespace NoFuture.Law.Tort.Tests
{
    /// <summary>
    /// Dougherty v. Stepp, 18 N.C. 371 (1835)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// Damage is NOT required for trespass to land
    /// ]]>
    /// </remarks>
    public class DoughertyvSteppTests
    {
        private readonly ITestOutputHelper output;

        public DoughertyvSteppTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void DoughertyvStepp()
        {
            var testSubject = new TrespassToLand
            {
                Consent = new Consent
                {
                    IsApprovalExpressed = lp => false,
                    IsCapableThereof = lp => lp is Dougherty
                },
                SubjectProperty = new RealProperty
                {
                    Name = "unenclosed land of plaintiff",
                    IsEntitledTo = lp => lp is Dougherty,
                    IsInPossessionOf = lp => lp is Dougherty
                },
                IsTangibleEntry = lp => lp is Stepp
            };

            var testResult = testSubject.IsValid(new Dougherty(), new Stepp());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
        }

        [Fact]
        public void TestIntangibleEntry()
        {
            var testSubject = new TrespassToLand
            {
                Consent = new Consent
                {
                    IsApprovalExpressed = lp => false,
                    IsCapableThereof = lp => lp is Dougherty
                },
                SubjectProperty = new RealProperty
                {
                    Name = "unenclosed land of plaintiff",
                    IsEntitledTo = lp => lp is Dougherty,
                    IsInPossessionOf = lp => lp is Dougherty
                },
                IsIntangibleEntry = lp => lp is Stepp,
                Causation = new Causation(ExtensionMethods.Tortfeasor)
                {
                    FactualCause = new FactualCause(ExtensionMethods.Tortfeasor)
                    {
                        IsButForCaused = lp => lp is Stepp
                    },
                    ProximateCause =  new ProximateCause(ExtensionMethods.Tortfeasor)
                    {
                        IsForeseeable = lp => lp is Stepp
                    }
                },
            };

            var testResult = testSubject.IsValid(new Dougherty(), new Stepp());
            this.output.WriteLine(testSubject.ToString());
            Assert.False(testResult);
            testSubject.ClearReasons();

            testSubject.Injury = new Damage(ExtensionMethods.Tortfeasor)
            {
                ToUsefulness = p => true
            };

            testResult = testSubject.IsValid(new Dougherty(), new Stepp());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
        }
    }

    public class Dougherty : LegalPerson, IPlaintiff
    {

    }
    public class Stepp : LegalPerson, ITortfeasor
    {

    }

}
