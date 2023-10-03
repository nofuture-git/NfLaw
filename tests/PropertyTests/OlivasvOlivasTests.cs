using System;
using System.Collections.Generic;
using System.Linq;
using NoFuture.Law.Property.US.FormsOf.InTerra;
using NoFuture.Law.Property.US.FormsOf.InTerra.Shared;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Property.Tests
{
    /// <summary>
    /// Olivas v. Olivas, 108 N.M. 814 (1989)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// 
    /// ]]>
    /// </remarks>
    public class OlivasvOlivasTests
    {
        private readonly ITestOutputHelper output;

        public OlivasvOlivasTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void OlivasvOlivas()
        {
            var testsubject = new TenancyInCommon
            {
                IsEqualRightToPossessWhole = lp => lp is SamOlivas || lp is CarolinaOlivas
            };


            var testResult = testsubject.IsValid(new SamOlivas(), new CarolinaOlivas());

            Assert.True(testResult);
            this.output.WriteLine(testsubject.ToString());

            var testSubject2 = new Ouster(testsubject) {IsVacated = p => p is SamOlivas};

            testResult = testSubject2.IsValid(new SamOlivas(), new CarolinaOlivas());

            Assert.False(testResult);
            this.output.WriteLine(testSubject2.ToString());

        }
    }

    public class SamOlivas : LegalPerson, IPlaintiff, ICotenant
    {
        public SamOlivas(): base("Sam Olivas") { }
    }

    public class CarolinaOlivas : LegalPerson, IDefendant, ICotenant
    {
        public CarolinaOlivas(): base("Carolina Olivas") { }
    }
}
