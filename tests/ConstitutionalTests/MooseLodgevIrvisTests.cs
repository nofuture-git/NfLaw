using System;
using System.Linq;
using NoFuture.Law.Constitutional.US;
using NoFuture.Law.Property.US.FormsOf.InTerra;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Constitutional.Tests
{
    /// <summary>
    /// Moose Lodge v. Irvis, 407 U.S. 163 (1972).
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// Purely private party in private setting and discrimation being had is trival doesn't count
    /// ]]>
    /// </remarks>
    public class MooseLodgevIrvisTests
    {
        private readonly ITestOutputHelper output;

        public MooseLodgevIrvisTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void MooseLodgevIrvis()
        {
            Func<ILegalPerson[], ILegalPerson> chargedWithDeprivation =
                lps => lps.FirstOrDefault(lp => lp is MooseLodge);


            var testSubject2 = new OperationOfPrivateClub()
            {
                FairlyDescribedAsStateActor = new StateAction2.TestIsStateActor(chargedWithDeprivation)
                {
                    IsTraditionalGovernmentFunction = a => !(a is OperationOfPrivateClub),
                }
            };

            var testResult2 = testSubject2.IsValid(new Irvis(), new MooseLodge());
            this.output.WriteLine(testSubject2.ToString());
            Assert.False(testResult2);
        }
    }

    public class OperationOfPrivateClub : StateAction2 { }

    public class MooseLodge : LegalPerson, IPlaintiff
    {
        public MooseLodge(): base("MooseLodge") { }
    }

    public class Irvis : LegalPerson, IDefendant
    {
        public Irvis(): base("Irvis") { }
    }
}
