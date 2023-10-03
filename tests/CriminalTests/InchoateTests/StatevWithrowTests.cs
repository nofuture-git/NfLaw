using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.Act;
using NoFuture.Law.Criminal.US.Elements.Intent.PenalCode;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.InchoateTests
{
    /// <summary>
    /// State v. Withrow, 8 S.W.3d 75 (1999)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, possession as actus reus failed because it was not a residence the defendant had sole control of
    /// ]]>
    /// </remarks>
    public class StatevWithrowTests
    {
        private readonly ITestOutputHelper output;

        public StatevWithrowTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void StatevWithrow()
        {
            var testCrime = new Felony
            {
                MensRea = new Purposely
                {
                    IsKnowledgeOfWrongdoing = lp => lp is Withrow,
                },
                ActusReus = new Possession()
            };

            var testResult = testCrime.IsValid(new Withrow());
            this.output.WriteLine(testCrime.ToString());
            Assert.False(testResult);
        }
    }

    public class Withrow : LegalPerson, IDefendant
    {
        public Withrow() : base("MICHAEL R. WITHROW") { }
    }
}
