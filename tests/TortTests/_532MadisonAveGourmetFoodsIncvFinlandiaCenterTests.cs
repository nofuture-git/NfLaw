﻿using System;
using Xunit;
using NoFuture.Law.US.Persons;
using NoFuture.Law.Tort.US.IntentionalTort;
using NoFuture.Law.US;
using Xunit.Abstractions;

namespace NoFuture.Law.Tort.Tests
{
    /// <summary>
    /// 532 Madison Avenue Gourmet Foods, Inc. v. Finlandia Center, 750 N.E.2d 1097 (N.Y. 2001)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, not public nuisance since everyone in the area lost money from having closed roads
    /// ]]>
    /// </remarks>
    public class _532MadisonAveGourmetFoodsIncvFinlandiaCenterTests
    {
        private readonly ITestOutputHelper output;

        public _532MadisonAveGourmetFoodsIncvFinlandiaCenterTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void _532MadisonAveGourmetFoodsIncvFinlandiaCenter()
        {
            var test = new PublicNuisance(ExtensionMethods.Tortfeasor)
            {
                IsUnreasonableInterference = lp => lp is FinlandiaCenter,
                IsRightCommonToPublic = lp => true,
                IsPrivatePeculiarInjury = lp => false
            };
            var testResult = test.IsValid(new _532MadisonAveGourmetFoodsInc(), new FinlandiaCenter());
            Assert.False(testResult);
            this.output.WriteLine(test.ToString());
        }
    }

    public class _532MadisonAveGourmetFoodsInc : LegalPerson, IPlaintiff
    {
        public _532MadisonAveGourmetFoodsInc(): base("532 Madison Ave Gourmet Foods, Inc.") { }
    }

    public class FinlandiaCenter : LegalPerson, ITortfeasor
    {
        public FinlandiaCenter(): base("Finlandia Center") { }
    }
}
