using System;
using NoFuture.Law.Criminal.US.Elements.AgainstProperty.Theft;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.TheftTests
{
    public class ExampleByReceivingTests
    {
        private readonly ITestOutputHelper output;

        public ExampleByReceivingTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ExampleByReceivingActTest()
        {
            var testAct = new ByReceiving
            {
                IsPresentStolen = lp => lp is ChanelFenceEg || lp is BurtThiefEg,
                IsTakenPossession = lp => lp is ChanelFenceEg || lp is BurtThiefEg || lp is SandraVictimEg,
                SubjectProperty = new LegalProperty("designer perfume"){ PropertyValue = dt => 5000m },
            };

            var testResult = testAct.IsValid(new ChanelFenceEg(), new BurtThiefEg(), new SandraVictimEg());
            this.output.WriteLine(testAct.ToString());
            Assert.True(testResult);
        }
    }

    public class ChanelFenceEg : LegalPerson, IDefendant
    {
        public ChanelFenceEg(): base("CHANEL FENCE") {  }
    }

    public class BurtThiefEg : LegalPerson, IDefendant
    {
        public BurtThiefEg() : base("BURT THIEF") { }
    }

    public class SandraVictimEg : LegalPerson, IVictim
    {
        public SandraVictimEg() : base("SANDRA VICTIM") {  }
    }
}
