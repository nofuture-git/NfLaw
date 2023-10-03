using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Defense.Excuse.Insanity;
using NoFuture.Law.Criminal.US.Elements.Act;
using NoFuture.Law.Criminal.US.Elements.Intent.ComLaw;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.DefenseTests.InsanityTests
{
    public class ExampleMNaghtenTests
    {
        private readonly ITestOutputHelper output;

        public ExampleMNaghtenTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ExampleMNaghtenFake()
        {
            var testCrime = new Felony
            {
                ActusReus = new ActusReus
                {
                    IsVoluntary = lp => lp is SusanEg,
                    IsAction = lp => lp is SusanEg
                },
                MensRea = new MaliceAforethought
                {
                    IsIntentOnWrongdoing = lp => lp is SusanEg
                }
            };

            var testResult = testCrime.IsValid(new SusanEg());
            Assert.True(testResult);

            var testSubject = new MNaghten
            {
                IsMentalDefect = lp => true,
                //by attempting to coverup action, insanity fails
                IsWrongnessOfAware = lp => true 
            };

            testResult = testSubject.IsValid(new SusanEg());
            this.output.WriteLine(testSubject.ToString());
            Assert.False(testResult);

        }

        [Fact]
        public void ExampleMNaghtenReal()
        {
            var testCrime = new Felony
            {
                ActusReus = new ActusReus
                {
                    IsVoluntary = lp => lp is AndreaEg,
                    IsAction = lp => lp is AndreaEg
                },
                MensRea = new MaliceAforethought
                {
                    IsIntentOnWrongdoing = lp => lp is AndreaEg
                }
            };

            var testResult = testCrime.IsValid(new AndreaEg());
            Assert.True(testResult);

            var testSubject = new MNaghten
            {
                IsMentalDefect = lp => true,
                IsNatureQualityOfAware = lp => false,
                IsWrongnessOfAware = lp => false
            };

            testResult = testSubject.IsValid(new AndreaEg());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
        }
    }

    public class AndreaEg : LegalPerson, IDefendant
    {
        public AndreaEg() : base("ANDREA SCHIZOPHRENIC") { }
    }

    public class SusanEg : LegalPerson, IDefendant
    {
        public SusanEg() : base("SUSAN INFANTCIDE") {}
    }
}
