using System;
using Xunit;
using NoFuture.Law.US.Persons;
using NoFuture.Law.Tort.US.ReasonableCare;
using NoFuture.Law.US;
using Xunit.Abstractions;

namespace NoFuture.Law.Tort.Tests
{
    /// <summary>
    /// Breunig v. American Family Ins. Co., 173 N.W.2d 619 (Wis. 1970)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// 
    /// ]]>
    /// </remarks>
    public class BreunigvAmericanFamilyInsCoTests
    {
        private readonly ITestOutputHelper output;

        public BreunigvAmericanFamilyInsCoTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void BreunigvAmericanFamilyInsCo()
        {
            var test = new OfMentalCapacity(ExtensionMethods.Tortfeasor)
            {
                IsMentallyIncapacitated = lp => lp is ErmaVeith,
                IsIncapacityForeseeable = lp => !(lp is ErmaVeith)
            };

            var testResult = test.IsValid(new AmericanFamilyInsCo(), new Breunig());
            Assert.True(testResult);
            this.output.WriteLine(test.ToString());

        }
    }

    public class Breunig : LegalPerson, IPlaintiff
    {
        public Breunig(): base("Breunig") { }
    }

    public class AmericanFamilyInsCo : ErmaVeith
    {
        public AmericanFamilyInsCo() : this("American Family Ins. Co.,") { }
        public AmericanFamilyInsCo(string name) : base (name) { }
    }

    public class ErmaVeith : LegalPerson, ITortfeasor
    {
        public ErmaVeith(): base("Erma Veith") { }
        public ErmaVeith(string name) :base(name) { }
    }
}
