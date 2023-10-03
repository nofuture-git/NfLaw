using System;
using NoFuture.Law.Property.US.FormsOf.InTerra;
using NoFuture.Law.Property.US.FormsOf.InTerra.Sequential;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Property.Tests
{
    /// <summary>
    /// Rucker v. Wynn, 212 Ga. App. 69 (Ga. Ct. App. 1994)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, commerical lease are in favor of landlord
    /// ]]>
    /// </remarks>
    public class RuckervWynnTests
    {
        private readonly ITestOutputHelper output;

        public RuckervWynnTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void RuckervWynn()
        {
            var lease = new Leasehold
            {
                SubjectProperty = new RealProperty("premises for a restaurant business")
                {
                    IsEntitledTo = lp => lp is Rucker
                },
                Inception = new DateTime(1990, 7, 1),
                Terminus = new DateTime(1995, 7, 1)
            };

            var testResult = lease.IsValid(new Rucker(), new Wynn());
            Assert.True(testResult);

            var test = new Eviction(lease)
            {
                CurrentDateTime = new DateTime(1994, 1, 1),
                //late rent
                IsBreachLeaseCondition = lp => lp is Wynn,
                IsResidenceHome = p => false,
                IsPeaceableSelfHelpReentry = lp => lp is Rucker
            };

            testResult = test.IsValid(new Rucker(), new Wynn());
            this.output.WriteLine(test.ToString());
            Assert.True(testResult);
        }
    }

    public class Rucker : LegalPerson, IPlaintiff, ILessor
    {
        public Rucker(): base("Rucker") { }
    }

    public class Wynn : LegalPerson, IDefendant, ILessee
    {
        public Wynn(): base("Wynn") { }
    }
}
