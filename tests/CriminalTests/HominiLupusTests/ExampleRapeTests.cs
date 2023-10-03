using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.AgainstPersons;
using NoFuture.Law.Criminal.US.Elements.AttendantCircumstances;
using NoFuture.Law.Criminal.US.Elements.Intent;
using NoFuture.Law.Criminal.US.Elements.Intent.PenalCode;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.HominiLupusTests
{
    public class ExampleRapeTests
    {
        private readonly ITestOutputHelper output;

        public ExampleRapeTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ExampleRape()
        {
            var testCrime = new Felony
            {
                ActusReus = new Rape
                {
                    IsByThreatOfViolence = lp => lp is AlexGamerEg,
                    IsSexualIntercourse = lp => lp is AlexGamerEg, 
                    IsOneOfTwo = lp => lp is AlexGamerEg,
                },
                MensRea = new Purposely
                {
                    IsIntentOnWrongdoing = lp => lp is AlexGamerEg
                }
            };

            var testResult = testCrime.IsValid(new AlexGamerEg());
            this.output.WriteLine(testCrime.ToString());
            Assert.True(testResult);
        }

        [Fact]
        public void TestLackOfConsent()
        {
            var testCrime = new Felony
            {
                ActusReus = new Rape
                {
                    IsSexualIntercourse = lp => lp is AlexGamerEg,
                    IsOneOfTwo = lp => lp is AlexGamerEg,
                    Consent = new VictimConsent
                    {
                        IsApprovalExpressed = lp => false,
                    }
                },
                MensRea = new Purposely
                {
                    IsIntentOnWrongdoing = lp => lp is AlexGamerEg
                }
            };

            var testResult = testCrime.IsValid(new AlexGamerEg(), new BrandySisterEg());
            this.output.WriteLine(testCrime.ToString());
            Assert.True(testResult);
        }

        [Fact]
        public void TestStatutoryRape()
        {
            var testCrime = new Felony
            {
                ActusReus = new Rape
                {
                    IsSexualIntercourse = lp => lp is AlexGamerEg,
                    IsOneOfTwo = lp => lp is AlexGamerEg,
                    Consent = new VictimConsent
                    {
                        IsCapableThereof = lp => ((lp as BrandySisterEg)?.Age  ?? 18) >= 16,
                    }
                },
                MensRea = new StrictLiability()
            };

            var testResult = testCrime.IsValid(new AlexGamerEg(), new BrandySisterEg());
            this.output.WriteLine(testCrime.ToString());
            Assert.True(testResult);
        }

        [Fact]
        public void TestConsentDefense()
        {
            var testCrime = new Felony
            {
                ActusReus = new Rape
                {
                    IsSexualIntercourse = lp => lp is AlexGamerEg,
                    IsOneOfTwo = lp => lp is AlexGamerEg,
                    Consent = new VictimConsent
                    {
                        //assented 
                        IsApprovalExpressed = lp => true,
                        IsCapableThereof = lp => ((lp as BrandySisterEg)?.Age ?? 18) >= 16,
                    }
                },
                MensRea = new Purposely
                {
                    IsIntentOnWrongdoing = lp => lp is AlexGamerEg
                }
            };

            var testResult = testCrime.IsValid(new AlexGamerEg(), new BrandySisterEg(){Age = 21});
            this.output.WriteLine(testCrime.ToString());
            Assert.False(testResult);
        }
    }

    public class AlexGamerEg : LegalPerson, IDefendant
    {
        public AlexGamerEg() : base("ALEX GAMER") { }
    }

    public class BradAlsogamerEg : LegalPerson
    {
        public BradAlsogamerEg(): base("BRAD ALSOGAMER") { }
    }

    public class BrandySisterEg : LegalPerson, IVictim
    {
        public BrandySisterEg() : base("BRANDY SISTER") { }

        public int Age { get; set; } = 14;
    }
}
