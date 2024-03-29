﻿using System;
using Xunit;
using NoFuture.Law.US.Persons;
using NoFuture.Law.Tort.US.UnintentionalTort;
using NoFuture.Law.US;
using Xunit.Abstractions;

namespace NoFuture.Law.Tort.Tests
{
    /// <summary>
    /// Cooley v. Public Service Co., 10 A.2d 673 (N.H. 1940)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, defendent cannot have mutually exclusive duty
    /// ]]>
    /// </remarks>
    public class CooleyvPublicServiceCoTests
    {
        private readonly ITestOutputHelper output;

        public CooleyvPublicServiceCoTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void CooleyvPublicServiceCo()
        {
            var test = new CritiqueLearnedHandsFormula(ExtensionMethods.Tortfeasor)
            {
                IsNegateExistingDuty = lp => lp is PublicServiceCo
            };

            var testResult = test.IsValid(new PublicServiceCo(), new Cooley());
            Assert.False(testResult);

            this.output.WriteLine(test.ToString());
        }
    }

    public class Cooley : LegalPerson, IPlaintiff
    {
        public Cooley(): base("Cooley") { }
    }

    public class PublicServiceCo : LegalPerson, ITortfeasor
    {
        public PublicServiceCo(): base("Public Service Co.") { }
    }
}
