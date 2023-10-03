using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Defense;
using NoFuture.Law.Criminal.US.Defense.Justification;
using NoFuture.Law.Criminal.US.Elements;
using NoFuture.Law.Criminal.US.Elements.Act;
using NoFuture.Law.Criminal.US.Elements.Intent.ComLaw;
using NoFuture.Law.Criminal.US.Terms;
using NoFuture.Law.Criminal.US.Terms.Violence;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.DefenseTests.DefenseOfOtherTests
{
    public class ExampleNecessityDefenseTests
    {
        private readonly ITestOutputHelper output;

        public ExampleNecessityDefenseTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ExampleNecessityDefense()
        {
            var testCrime = new Felony
            {
                ActusReus = new ActusReus
                {
                    IsAction = lp => lp is TamaraEg,
                    IsVoluntary = lp => lp is TamaraEg
                },
                MensRea = new GeneralIntent
                {
                    IsIntentOnWrongdoing = lp => lp is TamaraEg,
                    IsKnowledgeOfWrongdoing = lp => lp is TamaraEg
                }
            };

            var testResult = testCrime.IsValid(new TamaraEg());
            Assert.True(testResult);

            var testSubject = new NecessityDefense<ITermCategory>
            {
                Imminence = new Imminence(ExtensionMethods.Defendant)
                {
                    GetResponseTime = lp => Imminence.NormalReactionTimeToDanger
                },
                IsMultipleInHarm = lp => lp is TamaraEg,
                Proportionality = new ChoiceThereof<ITermCategory>(ExtensionMethods.Defendant)
                {
                    GetChoice = lp => new NondeadlyForce(),
                    GetOtherPossibleChoices = lp => new ITermCategory[]
                        {new SeriousBodilyInjury(), new DeadlyForce(), new Death()}
                }
            };
            testResult = testSubject.IsValid(new TamaraEg());
            this.output.WriteLine(testSubject.ToString());
            Assert.True(testResult);
        }
    }

    public class TamaraEg : LegalPerson, IDefendant
    {
        public TamaraEg() : base("TAMARA LOST") { }
    }
}
