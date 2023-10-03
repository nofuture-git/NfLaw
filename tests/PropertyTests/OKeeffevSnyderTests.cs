using System;
using NoFuture.Law.Property.US.Acquisition;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Property.Tests
{
    /// <summary>
    /// O’Keeffe v. Snyder, 83 N.J. 478 (1980)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, adverse possession for chattel is strange since its moveable and concealable
    /// ]]>
    /// </remarks>
    public class OKeeffevSnyderTests
    {
        private readonly ITestOutputHelper output;

        public OKeeffevSnyderTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void OKeeffevSnyder()
        {
            var test = new AdversePossession(ExtensionMethods.Disseisor)
            {
                Consent = Consent.NotGiven(),
                IsExclusivePossession = lp => lp is Snyder,
                IsContinuousPossession = lp => true,
                IsOpenNotoriousPossession = lp => false,
                SubjectProperty = new OKeeffePaintings()
                {
                    IsEntitledTo = lp => lp is OKeeffe,
                    IsInPossessionOf = lp => lp is Snyder
                },
                Inception = new DateTime(1972,6,1)
            };

            var testResult = test.IsValid(new OKeeffe(), new Snyder());
            this.output.WriteLine(test.ToString());
            Assert.False(testResult);
            
        }
    }

    public class OKeeffePaintings : LegalProperty
    {
        public OKeeffePaintings() : base("three small pictures painted by O’Keeffe") { }
    }

    public class OKeeffe : LegalPerson, IPlaintiff
    {
        public OKeeffe(): base("O’Keeffe") { }
    }

    public class Snyder : LegalPerson, IDefendant, IDisseisor
    {
        public Snyder(): base("Snyder") { }
    }
}
