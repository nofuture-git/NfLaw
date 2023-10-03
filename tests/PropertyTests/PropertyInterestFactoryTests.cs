﻿using System;
using NoFuture.Law.Property.US.FormsOf;
using NoFuture.Law.Property.US.FormsOf.InTerra;
using NoFuture.Law.Property.US.FormsOf.InTerra.Sequential;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using NUnit.Framework;

namespace NoFuture.Law.Property.Tests
{
    [TestFixture]
    public class PropertyInterestFactoryTests
    {
        [Test]
        public void TestApiLookAndStyle()
        {
            var test00 =
                new PropertyInterestFactory(new RealProperty("some land"), ExtensionMethods.Defendant)
                    .IsPresentInterestPossibleInfinite(lp => true, new CurtisLandholder()) as FeeSimpleFactory;

            Assert.IsNotNull(test00 );

            var test01 = test00.IsPresentInterestDefinitelyInfinite(lp => false, new CurtisLandholder()) as DefeasibleFeeFactory;

            Assert.IsNotNull(test01);

            var test02 =
                test01.IsFutureInterestInGrantor(lp => true, new CurtisLandholder()) as
                    PropertyInterestFactoryValue<FeeSimpleSubject2ExecutoryInterest>;

            Assert.IsNotNull(test02);

            var testResult = test02.GetValue();

            Assert.IsNotNull(testResult);

            Assert.IsInstanceOf<FeeSimpleSubject2ExecutoryInterest>(testResult);

            Console.WriteLine(test02.ToString());

        }

        [Test]
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
            Assert.IsNotNull(testResultFactory00);

            Assert.IsInstanceOf<FeeSimpleAbsolute>(testResultFactory00.GetValue());

            count = 0;
            test = new PropertyInterestFactory(new RealProperty("some land"), ExtensionMethods.Defendant);
            foreach (var p in FeeSimpleSubject2ExecutoryInterest.FactoryPaths[0])
            {
                test = test.GetNextFactory(count.ToString(), lp => p, curtis);
                count += 1;
            }

            var testResultFactory01 = test as PropertyInterestFactoryValue<FeeSimpleSubject2ExecutoryInterest>;
            Assert.IsNotNull(testResultFactory01);

            Assert.IsInstanceOf<FeeSimpleSubject2ExecutoryInterest>(testResultFactory01.GetValue());

            count = 0;
            test = new PropertyInterestFactory(new RealProperty("some land"), ExtensionMethods.Defendant);
            foreach (var p in FeeSimpleDeterminable.FactoryPaths[0])
            {
                test = test.GetNextFactory(count.ToString(), lp => p, curtis);
                count += 1;
            }

            var testResultFactory02 = test as PropertyInterestFactoryValue<FeeSimpleDeterminable>;
            Assert.IsNotNull(testResultFactory02);

            Assert.IsInstanceOf<FeeSimpleDeterminable>(testResultFactory02.GetValue());

            count = 0;
            test = new PropertyInterestFactory(new RealProperty("some land"), ExtensionMethods.Defendant);
            foreach (var p in FeeSimpleSubject2ConditionSubsequent.FactoryPaths[0])
            {
                test = test.GetNextFactory(count.ToString(), lp => p, curtis);
                count += 1;
            }

            var testResultFactory03 = test as PropertyInterestFactoryValue<FeeSimpleSubject2ConditionSubsequent>;
            Assert.IsNotNull(testResultFactory03);

            Assert.IsInstanceOf<FeeSimpleSubject2ConditionSubsequent>(testResultFactory03.GetValue());

            count = 0;
            test = new PropertyInterestFactory(new RealProperty("some land"), ExtensionMethods.Defendant);
            foreach (var p in Reversion.FactoryPaths[0])
            {
                test = test.GetNextFactory(count.ToString(), lp => p, curtis);
                count += 1;
            }

            var testResultFactory04 = test as PropertyInterestFactoryValue<Reversion>;
            Assert.IsNotNull(testResultFactory04);

            Assert.IsInstanceOf<Reversion>(testResultFactory04.GetValue());

            count = 0;
            test = new PropertyInterestFactory(new RealProperty("some land"), ExtensionMethods.Defendant);
            foreach (var p in VestedRemainderSubjectToOpen.FactoryPaths[0])
            {
                test = test.GetNextFactory(count.ToString(), lp => p, curtis);
                count += 1;
            }

            var testResultFactory05 = test as PropertyInterestFactoryValue<VestedRemainderSubjectToOpen>;
            Assert.IsNotNull(testResultFactory05);

            Assert.IsInstanceOf<VestedRemainderSubjectToOpen>(testResultFactory05.GetValue());

            count = 0;
            test = new PropertyInterestFactory(new RealProperty("some land"), ExtensionMethods.Defendant);
            foreach (var p in VestedRemainderSubjectToOpen.FactoryPaths[1])
            {
                test = test.GetNextFactory(count.ToString(), lp => p, curtis);
                count += 1;
            }

            var testResultFactory06 = test as PropertyInterestFactoryValue<VestedRemainderSubjectToOpen>;
            Assert.IsNotNull(testResultFactory06);

            Assert.IsInstanceOf<VestedRemainderSubjectToOpen>(testResultFactory06.GetValue());

            count = 0;
            test = new PropertyInterestFactory(new RealProperty("some land"), ExtensionMethods.Defendant);
            foreach (var p in ContingentRemainder.FactoryPaths[0])
            {
                test = test.GetNextFactory(count.ToString(), lp => p, curtis);
                count += 1;
            }

            var testResultFactory07 = test as PropertyInterestFactoryValue<ContingentRemainder>;
            Assert.IsNotNull(testResultFactory07);

            Assert.IsInstanceOf<ContingentRemainder>(testResultFactory07.GetValue());

            count = 0;
            test = new PropertyInterestFactory(new RealProperty("some land"), ExtensionMethods.Defendant);
            foreach (var p in ContingentRemainder.FactoryPaths[1])
            {
                test = test.GetNextFactory(count.ToString(), lp => p, curtis);
                count += 1;
            }

            var testResultFactory08 = test as PropertyInterestFactoryValue<ContingentRemainder>;
            Assert.IsNotNull(testResultFactory08);

            Assert.IsInstanceOf<ContingentRemainder>(testResultFactory08.GetValue());

            count = 0;
            test = new PropertyInterestFactory(new RealProperty("some land"), ExtensionMethods.Defendant);
            foreach (var p in AbsolutelyVestedRemainder.FactoryPaths[0])
            {
                test = test.GetNextFactory(count.ToString(), lp => p, curtis);
                count += 1;
            }

            var testResultFactory09 = test as PropertyInterestFactoryValue<AbsolutelyVestedRemainder>;
            Assert.IsNotNull(testResultFactory09);

            Assert.IsInstanceOf<AbsolutelyVestedRemainder>(testResultFactory09.GetValue());

            count = 0;
            test = new PropertyInterestFactory(new RealProperty("some land"), ExtensionMethods.Defendant);
            foreach (var p in VestedRemainderSubjectToDivestment.FactoryPaths[0])
            {
                test = test.GetNextFactory(count.ToString(), lp => p, curtis);
                count += 1;
            }

            var testResultFactory0A = test as PropertyInterestFactoryValue<VestedRemainderSubjectToDivestment>;
            Assert.IsNotNull(testResultFactory0A);

            Assert.IsInstanceOf<VestedRemainderSubjectToDivestment>(testResultFactory0A.GetValue());

            count = 0;
            test = new PropertyInterestFactory(new RealProperty("some land"), ExtensionMethods.Defendant);
            foreach (var p in VestedRemainderSubjectToDivestment.FactoryPaths[1])
            {
                test = test.GetNextFactory(count.ToString(), lp => p, curtis);
                count += 1;
            }

            var testResultFactory0B = test as PropertyInterestFactoryValue<VestedRemainderSubjectToDivestment>;
            Assert.IsNotNull(testResultFactory0B);

            Assert.IsInstanceOf<VestedRemainderSubjectToDivestment>(testResultFactory0B.GetValue());

        }

        public class CurtisLandholder : LegalPerson, IDefendant
        {

        }
    }
}
