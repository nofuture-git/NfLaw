using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.Act;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.MensReaTests
{
    /// <summary>
    /// State v. Horner, 126 Ohio St.3d 466, 2010-Ohio-3830
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, failure to object to defect (mens rea not said specifically) in indictment constitutes waiver of all but plain error.
    /// ]]>
    /// </remarks>
    public class StatevHornerTests
    {
        private readonly ITestOutputHelper output;

        public StatevHornerTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void StatevHorner()
        {
            var testSubject = new Felony
            {
                ActusReus = new ActusReus
                {
                    IsAction = lp => lp is Horner,
                    IsVoluntary = lp => lp is Horner
                },
                MensRea = null,
            };

            var testResult = testSubject.IsValid(new Horner());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
        }
    }

    public class Horner : LegalPerson, IDefendant
    {
        //public Horner():base("HORNER") { }
    }
}
