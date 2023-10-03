using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Defense.Excuse;
using NoFuture.Law.Criminal.US.Elements.Inchoate;
using NoFuture.Law.Criminal.US.Elements.Intent.ComLaw;
using Xunit;

namespace NoFuture.Law.Criminal.Tests.InchoateTests
{
    
    public class ExampleRenunciationTests
    {
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
            Assert.IsTrue(testResult);

            var testSubject = new Renunciation(testCrime)
            {
                IsCompletely = lp => lp is ShellyDriverEg,
                IsResultCrimeThwarted = lp => lp is ShellyDriverEg,
                IsVoluntarily = lp => lp is ShellyDriverEg
            };

            testResult = testSubject.IsValid(new ShellyDriverEg());
            Console.WriteLine(testSubject.ToString());
            Assert.IsTrue(testResult);
        }
    }
}
