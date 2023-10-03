using System;
using NoFuture.Law.Property.US.FormsOf.Intellectus;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Property.Tests
{
    /// <summary>
    /// Diamond v. Chakrabarty, 447 U.S. 303 (1980)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, being a alive does not preclude it from being patented
    /// ]]>
    /// </remarks>
    public class DiamondvChakrabartyTests
    {
        private readonly ITestOutputHelper output;

        public DiamondvChakrabartyTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void DiamondvChakrabarty()
        {
            var test = new PseudomonasBacterium()
            {
                IsSubjectToPatent = true,
                IsUseful = true,
                IsImplementable = true,
                IsAbstractIdea = false,
                IsLawOfNature = false,
                IsObviousIdea = false
            };
            var testResult = test.IsValid(new Diamond(), new Chakrabarty());
            Assert.True(testResult);
            this.output.WriteLine(test.ToString());
        }
    }

    public class PseudomonasBacterium : UtilityPatent
    {

    }

    public class Diamond : LegalPerson, IPlaintiff
    {
        public Diamond(): base("Diamond") { }
    }

    public class Chakrabarty : LegalPerson, IDefendant
    {
        public Chakrabarty(): base("Chakrabarty") { }
    }
}
