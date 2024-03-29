﻿using System;
using Xunit;
using NoFuture.Law.US.Persons;
using NoFuture.Law.Tort.US.UnintentionalTort;
using NoFuture.Law.US;
using Xunit.Abstractions;

namespace NoFuture.Law.Tort.Tests
{
    /// <summary>
    /// Uhr v. East Greenbush Central School District, 94 N.Y.2d 32 (1999)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// 
    /// ]]>
    /// </remarks>
    public class UhrvEastGreenbushCentralSchoolDistrictTests
    {
        private readonly ITestOutputHelper output;

        public UhrvEastGreenbushCentralSchoolDistrictTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void UhrvEastGreenbushCentralSchoolDistrict()
        {
            var test = new NegligenceByStatute(ExtensionMethods.Tortfeasor)
            {
                IsObeyStatute = lp => !(lp is EastGreenbushCentralSchoolDistrict),
                IsGroupMemberOfStatuesProtection = lp => lp is Uhr || lp is EastGreenbushCentralSchoolDistrict,
                IsDisobedienceCauseForAction = lp => false
            };

            var testResult = test.IsValid(new Uhr(), new EastGreenbushCentralSchoolDistrict());
            Assert.False(testResult);

            this.output.WriteLine(test.ToString());
        }
    }

    public class Uhr : LegalPerson, IPlaintiff
    {
        public Uhr(): base("Uhr") { }
    }

    public class EastGreenbushCentralSchoolDistrict : LegalPerson, ITortfeasor
    {
        public EastGreenbushCentralSchoolDistrict(): base("East Greenbush Central School District") { }
    }
}
