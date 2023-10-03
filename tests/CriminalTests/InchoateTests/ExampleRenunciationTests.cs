using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Defense.Excuse;
using NoFuture.Law.Criminal.US.Elements.Inchoate;
using NoFuture.Law.Criminal.US.Elements.Intent.ComLaw;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.InchoateTests
{
    public class ExampleRenunciationTests
    {
        private readonly ITestOutputHelper output;

        public ExampleRenunciationTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ExampleRenunciation()
        {
            var testCrime = new Felony
            {
                ActusReus = new Conspiracy
                {
                    IsAgreementToCommitCrime = lp => lp is ShellyDriverEg
                },
                MensRea = new SpecificIntent
                {
                    IsKnowledgeOfWrongdoing = lp => lp is ShellyDriverEg,
                    IsIntentOnWrongdoing = lp => lp is ShellyDriverEg
                }
            };
            var testResult = testCrime.IsValid(new ShellyDriverEg());
            Assert.True(testResult);

            var testSubject = new Renunciation(testCrime)
            {
                IsCompletely = lp => lp is ShellyDriverEg,
                IsResultCrimeThwarted = lp => lp is ShellyDriverEg,
                IsVoluntarily = lp => lp is ShellyDriverEg
            };

            testResult = testSubject.IsValid(new ShellyDriverEg());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
        }
    }
}
