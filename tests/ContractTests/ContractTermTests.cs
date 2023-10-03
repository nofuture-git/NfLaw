using System;
using System.Collections.Generic;
using System.Linq;
using NoFuture.Law.Contract.US.Terms;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Contract.Tests
{
    public class ContractTermTests
    {
        private readonly ITestOutputHelper output;

        public ContractTermTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestCompareTo()
        {
            var testSubject00 = new ContractTerm<object>("car parts", "car parts");
            var testSubject01 = new ContractTerm<object>("car parts", "car parts");

            var testResult = testSubject00.CompareTo(null);
            Assert.Equal(1, testResult);

            testResult = testSubject00.CompareTo(testSubject01);
            Assert.Equal(0, testResult);

            testSubject00.As(new WrittenTerm());
            testResult = testSubject00.CompareTo(testSubject01);
            Assert.Equal(-1, testResult);

            testResult = testSubject01.CompareTo(testSubject00);
            Assert.Equal(1, testResult);
        }

        [Fact]
        public void TestCompareTo_StdPrefInter()
        {
            var testSubject00 = new ContractTerm<object>("car parts", "car parts", new ExpressTerm());
            var testSubject01 = new ContractTerm<object>("car parts", "car parts", new ExpressTerm());

            var testResult = testSubject00.CompareTo(testSubject01);
            Assert.Equal(0, testResult);

            testSubject00 = new ContractTerm<object>("car parts", "car parts", new ExpressTerm());
            testSubject01 = new ContractTerm<object>("car parts", "car parts", new CourseOfPerformanceTerm());

            testResult = testSubject00.CompareTo(testSubject01);
            Assert.True(testResult < 0);

            testResult = testSubject01.CompareTo(testSubject00);
            Assert.True(testResult > 0);


            testSubject00 = new ContractTerm<object>("car parts", "car parts", new CourseOfPerformanceTerm());
            testSubject01 = new ContractTerm<object>("car parts", "car parts", new CourseOfDealingTerm());

            testResult = testSubject00.CompareTo(testSubject01);
            Assert.True(testResult < 0);

            testResult = testSubject01.CompareTo(testSubject00);
            Assert.True(testResult > 0);

            testSubject00 = new ContractTerm<object>("car parts", "car parts", new CourseOfDealingTerm());
            testSubject01 = new ContractTerm<object>("car parts", "car parts", new UsageOfTradeTerm());

            testResult = testSubject00.CompareTo(testSubject01);
            Assert.True(testResult < 0);

            testResult = testSubject01.CompareTo(testSubject00);
            Assert.True(testResult > 0);
        }

        [Fact]
        public void TestDecorator()
        {
            var testSubject = new Term<object>("test term", DBNull.Value);
            testSubject.As(new OralTerm()).As(new TechnicalTerm());

            var testResultName = testSubject.GetCategory();
            this.output.WriteLine(testResultName);

            var testResult = testSubject.IsCategory(new OralTerm());
            Assert.True(testResult);

            testResult = testSubject.IsCategory(typeof(TechnicalTerm));
            Assert.True(testResult);

            testResult = testSubject.IsCategory(typeof(CommonUseTerm));
            Assert.False(testResult);
        }

        [Fact]
        public void TestGetAdditionalTerms()
        {
            var terms00 = new HashSet<Term<object>>
            {
                new ContractTerm<object>("sell ranch", 0, new WrittenTerm()),
                new ContractTerm<object>("repurchase option", 1, new OralTerm())
            };
            var terms01 = new HashSet<Term<object>>
            {
                new ContractTerm<object>("sell ranch", 0, new WrittenTerm()),
            };

            var testResult = Term<object>.GetAdditionalTerms(terms00, terms01);

            Assert.NotNull(testResult);
            Assert.True(testResult.Any());

        }

        [Fact]
        public void TestGetInNameAgreedTerms()
        {
            var terms00 = new HashSet<Term<object>>
            {
                new ContractTerm<object>("sell ranch", 0, new WrittenTerm()),
                new ContractTerm<object>("repurchase option", 1, new OralTerm())
            };
            var terms01 = new HashSet<Term<object>>
            {
                new ContractTerm<object>("sell ranch", 0, new WrittenTerm()),
            };

            var testResult = Term<object>.GetInNameAgreedTerms(terms00, terms01);

            Assert.NotNull(testResult);
            Assert.True(testResult.Any());
            Assert.True(testResult.Count == 1);
            Assert.Equal("sell ranch", testResult.First().Name);
        }

        [Fact]
        public void TestGetAgreedTerms()
        {
            var terms00 = new HashSet<Term<object>>
            {
                new ContractTerm<object>("sell ranch", 0, new WrittenTerm()),
                new ContractTerm<object>("repurchase option", 1, new OralTerm()),
                new ContractTerm<object>("wrong value", 12, new OralTerm()),
            };
            var terms01 = new HashSet<Term<object>>
            {
                new ContractTerm<object>("sell ranch", 0, new WrittenTerm()),
                new ContractTerm<object>("wrong name", 1),
                new ContractTerm<object>("wrong value", 11, new OralTerm()),
            };

            var testResult = Term<object>.GetAgreedTerms(terms00, terms01);

            Assert.NotNull(testResult);
            Assert.True(testResult.Any());
            Assert.True(testResult.Count == 1);
            Assert.Equal("sell ranch", testResult.First().Name);
        }

        [Fact]
        public void TestThrowOnDupRefersTo()
        {
            var terms01 = new HashSet<Term<object>>
            {
                new ContractTerm<object>("first name", 0, new WrittenTerm()),
                new ContractTerm<object>("second name", 0),
                new ContractTerm<object>("term 00", 11, new OralTerm()),
                new ContractTerm<object>("term 01", 12, new OralTerm()),
                new ContractTerm<object>("term 02", 13, new OralTerm()),
            };
            Assert.Throws<AggregateException>(() => Term<object>.ThrowOnDupRefersTo(terms01));
        }
    }
}
