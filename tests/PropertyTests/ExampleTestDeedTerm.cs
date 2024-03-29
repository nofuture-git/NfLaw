﻿using NoFuture.Law.Property.US.FormsOf.InTerra;
using NoFuture.Law.Property.US.Terms;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Property.Tests
{
    public class ExampleTestDeedTerm
    {
        private readonly ITestOutputHelper output;

        public ExampleTestDeedTerm(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestDeedTermIsValid()
        {
            var property = new ExampleParcelOfLand
            {
                IsEntitledTo = lp => lp is ExampleOfferor,
                IsInPossessionOf = lp => lp is ExampleOfferor,
                Name = "Black Acre"
            };

            var testSubject = new DeedTerm("Deed to Black Acre", property)
            {
                IsSigned = lp => lp is ExampleOfferor || lp is ExampleOfferee,
                IsWritten = lp => true
            };

            var testResult = testSubject.IsValid(new ExampleOfferor(), new ExampleOfferee());
            Assert.True(testResult);

        }
    }

    public class ExampleParcelOfLand : RealProperty { }

    public class ExampleOfferor : LegalPerson, IOfferor
    {
        public ExampleOfferor() :base("Offeror") { }
    }

    public class ExampleOfferee : LegalPerson, IOfferee
    {
        public ExampleOfferee() : base("Offeree") { }
    }
}
