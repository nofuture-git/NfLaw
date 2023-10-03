using System;
using NoFuture.Law.Property.US.FormsOf;
using NoFuture.Law.Property.US.FormsOf.InTerra;
using NoFuture.Law.Property.US.FormsOf.InTerra.Sequential;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Property.Tests
{
    public class PropertyInterestFactoryTests
    {
        private readonly ITestOutputHelper output;

        public PropertyInterestFactoryTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestApiLookAndStyle()
        {
            var test00 =
                new PropertyInterestFactory(new RealProperty("some land"), ExtensionMethods.Defendant)
                    .IsPresentInterestPossibleInfinite(lp => true, new CurtisLandholder()) as FeeSimpleFactory;

            Assert.NotNull(test00 );

            var test01 = test00.IsPresentInterestDefinitelyInfinite(lp => false, new CurtisLandholder()) as DefeasibleFeeFactory;

            Assert.NotNull(test01);

            var test02 =
                test01.IsFutureInterestInGrantor(lp => true, new CurtisLandholder()) as
                    PropertyInterestFactoryValue<FeeSimpleSubject2ExecutoryInterest>;

            Assert.NotNull(test02);

            var testResult = test02.GetValue();

            Assert.NotNull(testResult);

            Assert.IsAssignableFrom<FeeSimpleSubject2ExecutoryInterest>(testResult);

            this.output.WriteLine(test02.ToString());

        }

        [Fact]
        public void TestAllPaths()
        {
            IPropertyInterestFactory test =
                new PropertyInterestFactory(new RealProperty("some land"), ExtensionMethods.Defendant);
            var curtis = new CurtisLandholder();
            var count = 0;

            foreach (var p in FeeSimpleAbsolute.FactoryPaths[0])
            {
                test = test.GetNextFactory(count.ToString(), lp => p, curtis);
                count += 1;
            }

            var testResultFactory00 = test as PropertyInterestFactoryValue<FeeSimpleAbsolute>;
            Assert.NotNull(testResultFactory00);

            Assert.IsAssignableFrom<FeeSimpleAbsolute>(testResultFactory00.GetValue());

            count = 0;
            test = new PropertyInterestFactory(new RealProperty("some land"), ExtensionMethods.Defendant);
            foreach (var p in FeeSimpleSubject2ExecutoryInterest.FactoryPaths[0])
            {
                test = test.GetNextFactory(count.ToString(), lp => p, curtis);
                count += 1;
            }

            var testResultFactory01 = test as PropertyInterestFactoryValue<FeeSimpleSubject2ExecutoryInterest>;
            Assert.NotNull(testResultFactory01);

            Assert.IsAssignableFrom<FeeSimpleSubject2ExecutoryInterest>(testResultFactory01.GetValue());

            count = 0;
            test = new PropertyInterestFactory(new RealProperty("some land"), ExtensionMethods.Defendant);
            foreach (var p in FeeSimpleDeterminable.FactoryPaths[0])
            {
                test = test.GetNextFactory(count.ToString(), lp => p, curtis);
                count += 1;
            }

            var testResultFactory02 = test as PropertyInterestFactoryValue<FeeSimpleDeterminable>;
            Assert.NotNull(testResultFactory02);

            Assert.IsAssignableFrom<FeeSimpleDeterminable>(testResultFactory02.GetValue());

            count = 0;
            test = new PropertyInterestFactory(new RealProperty("some land"), ExtensionMethods.Defendant);
            foreach (var p in FeeSimpleSubject2ConditionSubsequent.FactoryPaths[0])
            {
                test = test.GetNextFactory(count.ToString(), lp => p, curtis);
                count += 1;
            }

            var testResultFactory03 = test as PropertyInterestFactoryValue<FeeSimpleSubject2ConditionSubsequent>;
            Assert.NotNull(testResultFactory03);

            Assert.IsAssignableFrom<FeeSimpleSubject2ConditionSubsequent>(testResultFactory03.GetValue());

            count = 0;
            test = new PropertyInterestFactory(new RealProperty("some land"), ExtensionMethods.Defendant);
            foreach (var p in Reversion.FactoryPaths[0])
            {
                test = test.GetNextFactory(count.ToString(), lp => p, curtis);
                count += 1;
            }

            var testResultFactory04 = test as PropertyInterestFactoryValue<Reversion>;
            Assert.NotNull(testResultFactory04);

            Assert.IsAssignableFrom<Reversion>(testResultFactory04.GetValue());

            count = 0;
            test = new PropertyInterestFactory(new RealProperty("some land"), ExtensionMethods.Defendant);
            foreach (var p in VestedRemainderSubjectToOpen.FactoryPaths[0])
            {
                test = test.GetNextFactory(count.ToString(), lp => p, curtis);
                count += 1;
            }

            var testResultFactory05 = test as PropertyInterestFactoryValue<VestedRemainderSubjectToOpen>;
            Assert.NotNull(testResultFactory05);

            Assert.IsAssignableFrom<VestedRemainderSubjectToOpen>(testResultFactory05.GetValue());

            count = 0;
            test = new PropertyInterestFactory(new RealProperty("some land"), ExtensionMethods.Defendant);
            foreach (var p in VestedRemainderSubjectToOpen.FactoryPaths[1])
            {
                test = test.GetNextFactory(count.ToString(), lp => p, curtis);
                count += 1;
            }

            var testResultFactory06 = test as PropertyInterestFactoryValue<VestedRemainderSubjectToOpen>;
            Assert.NotNull(testResultFactory06);

            Assert.IsAssignableFrom<VestedRemainderSubjectToOpen>(testResultFactory06.GetValue());

            count = 0;
            test = new PropertyInterestFactory(new RealProperty("some land"), ExtensionMethods.Defendant);
            foreach (var p in ContingentRemainder.FactoryPaths[0])
            {
                test = test.GetNextFactory(count.ToString(), lp => p, curtis);
                count += 1;
            }

            var testResultFactory07 = test as PropertyInterestFactoryValue<ContingentRemainder>;
            Assert.NotNull(testResultFactory07);

            Assert.IsAssignableFrom<ContingentRemainder>(testResultFactory07.GetValue());

            count = 0;
            test = new PropertyInterestFactory(new RealProperty("some land"), ExtensionMethods.Defendant);
            foreach (var p in ContingentRemainder.FactoryPaths[1])
            {
                test = test.GetNextFactory(count.ToString(), lp => p, curtis);
                count += 1;
            }

            var testResultFactory08 = test as PropertyInterestFactoryValue<ContingentRemainder>;
            Assert.NotNull(testResultFactory08);

            Assert.IsAssignableFrom<ContingentRemainder>(testResultFactory08.GetValue());

            count = 0;
            test = new PropertyInterestFactory(new RealProperty("some land"), ExtensionMethods.Defendant);
            foreach (var p in AbsolutelyVestedRemainder.FactoryPaths[0])
            {
                test = test.GetNextFactory(count.ToString(), lp => p, curtis);
                count += 1;
            }

            var testResultFactory09 = test as PropertyInterestFactoryValue<AbsolutelyVestedRemainder>;
            Assert.NotNull(testResultFactory09);

            Assert.IsAssignableFrom<AbsolutelyVestedRemainder>(testResultFactory09.GetValue());

            count = 0;
            test = new PropertyInterestFactory(new RealProperty("some land"), ExtensionMethods.Defendant);
            foreach (var p in VestedRemainderSubjectToDivestment.FactoryPaths[0])
            {
                test = test.GetNextFactory(count.ToString(), lp => p, curtis);
                count += 1;
            }

            var testResultFactory0A = test as PropertyInterestFactoryValue<VestedRemainderSubjectToDivestment>;
            Assert.NotNull(testResultFactory0A);

            Assert.IsAssignableFrom<VestedRemainderSubjectToDivestment>(testResultFactory0A.GetValue());

            count = 0;
            test = new PropertyInterestFactory(new RealProperty("some land"), ExtensionMethods.Defendant);
            foreach (var p in VestedRemainderSubjectToDivestment.FactoryPaths[1])
            {
                test = test.GetNextFactory(count.ToString(), lp => p, curtis);
                count += 1;
            }

            var testResultFactory0B = test as PropertyInterestFactoryValue<VestedRemainderSubjectToDivestment>;
            Assert.NotNull(testResultFactory0B);

            Assert.IsAssignableFrom<VestedRemainderSubjectToDivestment>(testResultFactory0B.GetValue());

        }

        public class CurtisLandholder : LegalPerson, IDefendant
        {

        }
    }
}
