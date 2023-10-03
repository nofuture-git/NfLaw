using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.AgainstProperty.Theft;
using NoFuture.Law.Criminal.US.Elements.Intent.PenalCode;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.TheftTests
{
    /// <summary>
    /// State v. Robertson, 531 S. E. 2d 490 (2000)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, without violence of force, its larceny not robbery
    /// ]]>
    /// </remarks>
    public class StatevRobertsonTests
    {
        private readonly ITestOutputHelper output;

        public StatevRobertsonTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void StatevRobertson()
        {
            var testCrime = new Felony
            {
                ActusReus = new Robbery
                {
                    SubjectProperty = new LegalProperty("purse")
                    {
                        IsEntitledTo = lp => lp is MsDover,
                        IsInPossessionOf = lp => lp is MsDover
                    },
                    IsAsportation = lp => lp is Robertson,
                    IsTakenPossession = lp => lp is Robertson,
                    //court rules this is false, snatching is larceny not robbery 
                    IsByViolence = lp => false,
                },
                MensRea = new Purposely
                {
                    IsIntentOnWrongdoing = lp => lp is Robertson
                }
            };
            var testResult = testCrime.IsValid(new Robertson(), new MsDover());
            this.output.WriteLine(testCrime.ToString());
            Assert.False(testResult);
        }
    }

    public class Robertson : LegalPerson, IDefendant
    {
        public Robertson() : base("WILLIE HERBERT ROBERTSON") {  }

    }

    public class MsDover : LegalPerson, IVictim
    {
        public MsDover() : base("MS. DOVER") {  }
    }
}
