using System;
using System.Collections.Generic;
using System.Linq;
using NoFuture.Law.Property.US.FormsOf.InTerra.Shared;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Property.Tests
{
    /// <summary>
    /// Riddle v. Harmon, 102 Cal.App.3d 524 (Cal. App. 1980).
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// 
    /// ]]>
    /// </remarks>
    public class RiddlevHarmonTests
    {
        private readonly ITestOutputHelper output;

        public RiddlevHarmonTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void RiddlevHarmon()
        {
            var testSubject = new JointTenancy()
            {
                InterestFraction = lp => 0.5D,
                IsEqualRightToPossessWhole = lp => lp is Riddle || lp is Harmon,
                InterestCreationInstrument = lp => new TitleToParcelOFRealEstate(),
                InterestCreationDate = lp => lp is Riddle ? new DateTime(1975,12,8) : new DateTime(1950,1,1) //case doesn't actually give this date
            };

            var testResult = testSubject.IsValid(new Riddle(), new Harmon());
            Assert.False(testResult);
            this.output.WriteLine(testSubject.ToString());
        }
    }

    public class TitleToParcelOFRealEstate : LegalConcept
    {
        public override bool IsValid(params ILegalPerson[] persons)
        {
            return true;
        }

        public override bool Equals(object obj)
        {
            return obj is TitleToParcelOFRealEstate;
        }
    }

    public class Riddle : LegalPerson, IPlaintiff, ICotenant
    {
        public Riddle(): base("Frances Riddle") { }
    }

    public class Harmon : LegalPerson, IDefendant, ICotenant
    {
        public Harmon(): base("Harmon") { }
    }
}
