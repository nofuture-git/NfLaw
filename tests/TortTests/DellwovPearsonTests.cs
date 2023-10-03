using System;
using Xunit;
using NoFuture.Law.US.Persons;
using NoFuture.Law.Tort.US.ReasonableCare;
using NoFuture.Law.US;
using Xunit.Abstractions;

namespace NoFuture.Law.Tort.Tests
{
    /// <summary>
    /// Dellwo v. Pearson, 107 N.W.2d 859 (Minn. 1961)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, liability for children is typically not applicable unless they are performing some adult activity
    /// ]]>
    /// </remarks>
    public class DellwovPearsonTests
    {
        private readonly ITestOutputHelper output;

        public DellwovPearsonTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void DellwovPearson()
        {
            var test = new OfChildren(ExtensionMethods.Tortfeasor)
            {
                GetActionOfPerson = lp => lp is Pearson ? new OperateMotorBoat() : null,
                IsDangerousAdultActivity = act => act is OperateMotorBoat,
                IsExercisedAdultCare = lp => !(lp is Pearson)
            };
            var testResult = test.IsValid(new Dellwo(), new Pearson());
            Assert.False(testResult);
            this.output.WriteLine(test.ToString());
        }
    }

    public class OperateMotorBoat : LegalConcept, IAct
    {
        public override bool IsValid(params ILegalPerson[] persons)
        {
            return true;
        }

        public Predicate<ILegalPerson> IsVoluntary { get; set; } = lp => false;
        public Predicate<ILegalPerson> IsAction { get; set; } = lp => false;
        public Duty Duty { get; set; }
    }

    public class Dellwo : LegalPerson, IPlaintiff
    {
        public Dellwo(): base("Jeanette E. Dellwo") { }
    }

    public class Pearson : LegalPerson, ITortfeasor, IChild
    {
        public Pearson(): base("Pearson") { }
    }
}
