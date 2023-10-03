using System;
using NoFuture.Law.Property.US.Acquisition;
using NoFuture.Law.Property.US.FormsOf.Intellectus;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Property.Tests
{
    /// <summary>
    /// Suntrust Bank v. Houghton Mifflin Co., 268 F.3d 1257 (11th Cir. 2001)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, parody is a form of fair use
    /// ]]>
    /// </remarks>
    public class SuntrustBankvHoughtonMifflinCoTests
    {
        private readonly ITestOutputHelper output;

        public SuntrustBankvHoughtonMifflinCoTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void SuntrustBankvHoughtonMifflinCo()
        {
            var test = new FairUse(ExtensionMethods.Defendant)
            {
                IsParody = lp => lp is HoughtonMifflinCo,
                SubjectProperty = new GWTW
                {
                    IsEntitledTo = lp => lp is SuntrustBank,
                    IsInPossessionOf = lp => lp is SuntrustBank,
                    IsOriginalExpression = true,
                    IsMinimalCreative = true
                }
            };

            var testResult = test.IsValid(new SuntrustBank(), new HoughtonMifflinCo());
            Assert.True(testResult);
            this.output.WriteLine(test.ToString());
        }
    }

    public class GWTW : Copyright
    {
        public GWTW() : base("Gone With The Wind") { }
    }

    public class TWDG : Copyright
    {
        public TWDG(): base("The Wind Done Gone") { }
    }

    public class SuntrustBank : LegalPerson, IPlaintiff
    {
        public SuntrustBank(): base("Suntrust Bank") { }
    }

    public class HoughtonMifflinCo : LegalPerson, IDefendant
    {
        public HoughtonMifflinCo(): base("Houghton Mifflin Co.") { }
    }
}
