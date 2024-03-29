﻿using System;
using NoFuture.Law.Criminal.US.Terms.Violence;
using Xunit;
using NoFuture.Law.US.Persons;
using NoFuture.Law.US;
using NoFuture.Law.Criminal.US.Defense.Justification;
using Xunit.Abstractions;

namespace NoFuture.Law.Tort.Tests
{
    /// <summary>
    /// Ploof v. Putnam, 71 A. 188 (Vt. 1908)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctine issue, necessity defense again 
    /// ]]>
    /// </remarks>
    public class PutnamvPloofTests
    {
        private readonly ITestOutputHelper output;

        public PutnamvPloofTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void PutnamvPloof()
        {
            var test =
                new NecessityDefense<ITermCategory>(ExtensionMethods.Tortfeasor)
                {
                    IsMultipleInHarm = lp => true,
                    IsResponsibleForSituationArise = lp => false,
                    Proportionality = new ChoiceThereof<ITermCategory>(ExtensionMethods.Tortfeasor)
                    {
                        GetChoice = lp => new MoorTheSloop(),
                        GetOtherPossibleChoices = lp => new ITermCategory[] {new SeriousBodilyInjury(),},
                    }
                };
            var testResult = test.IsValid(new Putnam(), new Ploof());
            this.output.WriteLine(test.ToString());
            Assert.True(testResult);
        }
    }

    public class MoorTheSloop : TermCategory
    {
        protected override string CategoryName => "moor the sloop to defendant’s dock";
    }

    public class Putnam : LegalPerson, ITortfeasor
    {
        public Putnam(): base("") { }
    }

    public class Ploof : LegalPerson, IPlaintiff
    {
        public Ploof(): base("") { }
    }
}
