using System;
using NoFuture.Law.Property.US.FormsOf;
using NoFuture.Law.Tort.US.Defense;
using Xunit;
using NoFuture.Law.US.Persons;
using NoFuture.Law.US;
using Xunit.Abstractions;

namespace NoFuture.Law.Tort.Tests
{
    /// <summary>
    /// Gortarez v. Smitty’s Super Valu, 680 P.2d 807 (Ariz. 1984)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue: shopkeeper's privilege
    /// ]]>
    /// </remarks>
    public class GortarezvSmittysSuperValuTests
    {
        private readonly ITestOutputHelper output;

        public GortarezvSmittysSuperValuTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void GortarezvSmittysSuperValu()
        {
            var test = new ShopkeeperPrivilege
            {
                IsReasonableCause = lp => !(lp is SmittysSuperValu),
                IsReasonableManner = lp => !(lp is SmittysSuperValu),
                IsReasonableTime = lp => true,
                SubjectProperty = new AirFreshener(),
                Consent =  new Consent(ExtensionMethods.Tortfeasor)
                {
                    IsCapableThereof = lp => lp is SmittysSuperValu,
                    IsApprovalExpressed = lp => false
                }
            };
            var testResult = test.IsValid(new SmittysSuperValu(), new Gortarez());
            this.output.WriteLine(test.ToString());
            Assert.False(testResult);

        }
    }

    public class AirFreshener : PersonalProperty
    {
        
        public override Predicate<ILegalPerson> IsEntitledTo { get; set; } = lp => lp is SmittysSuperValu;
        public override Predicate<ILegalPerson> IsInPossessionOf { get; set; } = lp => lp is SmittysSuperValu;
        public override Func<DateTime?, decimal?> PropertyValue { get; set; } = dt => 0.56m;
    }

    public class Gortarez : LegalPerson, IPlaintiff
    {
        public Gortarez(): base("") { }
    }

    public class SmittysSuperValu : LegalPerson, IMerchant<ILegalConcept>, ITortfeasor, IVictim
    {
        public SmittysSuperValu(): base("") { }
        public Predicate<ILegalConcept> IsSkilledOrKnowledgeableOf { get; set; } = g => true;
    }
}
