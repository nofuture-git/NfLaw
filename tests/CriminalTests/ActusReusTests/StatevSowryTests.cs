﻿using System;
using NoFuture.Law.Criminal.US;
using NoFuture.Law.Criminal.US.Elements.Act;
using NoFuture.Law.Criminal.US.Elements.Intent.PenalCode;
using NoFuture.Law.US;
using NoFuture.Law.US.Persons;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.ActusReusTests
{
    /// <summary>
    /// State v. Sowry, 155 Ohio App. 3d 742 (2004)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, one cannot act voluntarily while under the direct control of arresting officer
    /// ]]>
    /// </remarks>
    public class StatevSowryTests
    {
        private readonly ITestOutputHelper output;

        public StatevSowryTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void StatevSowry()
        {
            var testSubject = new Felony
            {
                ActusReus = new ActusReus
                {
                    IsVoluntary = lp => false,
                    IsAction = lp => lp is Sowry,
                },
                MensRea = new Knowingly
                {
                    IsKnowledgeOfWrongdoing = lp => lp is Sowry,
                }
            };

            var testResult = testSubject.IsValid(new Sowry());
            this.output.WriteLine(testSubject.ToString());
            Assert.False(testResult);
        }
    }

    public class Sowry : LegalPerson, IDefendant
    {

    }
}
