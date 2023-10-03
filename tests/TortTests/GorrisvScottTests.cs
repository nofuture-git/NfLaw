using System;
using Xunit;
using NoFuture.Law.US.Persons;
using NoFuture.Law.Tort.US.Elements;
using NoFuture.Law.Tort.US.UnintentionalTort;
using NoFuture.Law.US;

namespace NoFuture.Law.Tort.Tests
{
    /// <summary>
    /// Gorris v. Scott, [1874] L.R. 9 Ex. 125
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, demo of non-negligence when attempting to use a statute's disobedience completely out of context
    /// ]]>
    /// </remarks>
    
    public class GorrisvScottTests
    {
        [Fact]
        public void GorrisvScott()
        {
            var test = new NegligenceByStatute(ExtensionMethods.Tortfeasor)
            {
                IsObeyStatute = lp => !(lp is Scott),
                IsGroupMemberOfStatuesProtection = lp => !(lp is Scott) && !(lp is Gorris)
            };

            var testResult = test.IsValid(new Gorris(), new Scott());
            Assert.IsFalse(testResult);

            Console.WriteLine(test.ToString());
        }
    }

    public class Gorris : LegalPerson, IPlaintiff
    {
        public Gorris(): base("Gorris") { }
    }

    public class Scott : LegalPerson, ITortfeasor
    {
        public Scott(): base("Scott") { }
    }
}
