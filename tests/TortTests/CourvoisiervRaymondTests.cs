using System;
using NoFuture.Law.Criminal.US.Defense.Justification;
using Xunit;
using NoFuture.Law.US.Persons;
using NoFuture.Law.US;
using Xunit.Abstractions;

namespace NoFuture.Law.Tort.Tests
{
    /// <summary>
    /// Courvoisier v. Raymond, 47 P. 284 (Colo. 1896)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctine issue: reasonable error in self-defense
    /// ]]>
    /// </remarks>
    public class CourvoisiervRaymondTests
    {
        private readonly ITestOutputHelper output;

        public CourvoisiervRaymondTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void CourvoisiervRaymond()
        {
            var test = new DefenseOfSelf(ExtensionMethods.Tortfeasor)
            {
                IsReasonableFearOfInjuryOrDeath = lp => lp is Courvoisier,
                Proportionality = new Proportionality<ITermCategory>(ExtensionMethods.Tortfeasor)
                {
                    GetChoice = lp => new PullRevolverShoot(),
                    IsProportional = (t1, t2) => t1.Equals(t2),
                },
            };

            var testResult = test.IsValid(new Courvoisier(), new Raymond());
            this.output.WriteLine(test.ToString());
            Assert.True(testResult);

        }
    }

    public class PullRevolverShoot : TermCategory
    {
        protected override string CategoryName => "blasted varmit";

        public override bool Equals(object obj)
        {
            return obj is PullRevolverShoot;
        }
    }

    public class Courvoisier : LegalPerson, ITortfeasor
    {
        public Courvoisier(): base("") { }
    }

    public class Raymond : LegalPerson, IPlaintiff
    {
        public Raymond(): base("") { }
    }
}
