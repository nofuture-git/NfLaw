using System;
using NoFuture.Law.Criminal.US.Defense.Justification;
using Xunit;
using NoFuture.Law.US.Persons;
using NoFuture.Law.US;
using NoFuture.Law.Criminal.US.Terms;
using NoFuture.Law.Criminal.US.Terms.Violence;
using Xunit.Abstractions;

namespace NoFuture.Law.Tort.Tests
{
    /// <summary>
    /// Katko v. Briney, 183 N.W.2d 657 (Iowa 1971)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, same as Bird v Holbrook, except Katko is a thief - still no allowed to use
    /// deadly force in defense of property.
    /// ]]>
    /// </remarks>
    public class KatkovBrineyTests
    {
        private readonly ITestOutputHelper output;

        public KatkovBrineyTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void KatkovBriney()
        {
            var test = new DefenseOfProperty(ExtensionMethods.Tortfeasor)
            {
                Proportionality = new Proportionality<ITermCategory>(ExtensionMethods.Tortfeasor)
                {
                    GetChoice = lp =>
                    {
                        if (lp is Briney)
                            return new SeriousBodilyInjury();
                        return new NondeadlyForce();
                    }
                }
            };

            var testResult = test.IsValid(new Briney(), new Katko());
            this.output.WriteLine(test.ToString());
            Assert.False(testResult);
        }
    }

    public class Katko : LegalPerson, IPlaintiff
    {
        public Katko(): base("") { }
    }

    public class Briney : LegalPerson, ITortfeasor
    {
        public Briney(): base("") { }
    }
}
