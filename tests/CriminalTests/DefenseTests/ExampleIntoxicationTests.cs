using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Defense.Excuse;
using NoFuture.Law.Criminal.US.Elements.Act;
using NoFuture.Law.Criminal.US.Elements.Intent.ComLaw;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.DefenseTests
{
    public class ExampleIntoxicationTests
    {
        private readonly ITestOutputHelper output;

        public ExampleIntoxicationTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ExampleIntoxication()
        {
            var testCrime = new Felony
            {
                ActusReus = new ActusReus
                {
                    IsAction = lp => lp is DelilahEg,
                    IsVoluntary = lp => lp is DelilahEg,
                },
                MensRea = new GeneralIntent
                {
                    IsIntentOnWrongdoing = lp => lp is DelilahEg,
                }
            };

            var testResult = testCrime.IsValid(new DelilahEg());
            Assert.True(testResult);

            var testSubject = new Intoxication
            {
                //the ruffee is taken unknowingly
                IsInvoluntary = lp => lp is DelilahEg,
                IsIntoxicated = lp => lp is DelilahEg
            };

            testResult = testSubject.IsValid(new DelilahEg());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
        }
    }

    public class DelilahEg : LegalPerson, IDefendant
    {
        public DelilahEg() : base("DELILAH RUFFEE")
        {

        }
    }
}
