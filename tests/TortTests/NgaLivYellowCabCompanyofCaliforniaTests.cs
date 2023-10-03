using System;
using Xunit;
using NoFuture.Law.US.Persons;
using NoFuture.Law.Tort.US.Elements;
using NoFuture.Law.Tort.US.UnintentionalTort;
using NoFuture.Law.US;
using Xunit.Abstractions;

namespace NoFuture.Law.Tort.Tests
{
    /// <summary>
    /// Li v. Yellow Cab Company of California, 532 P.2d 1226 (Cal. 1975)
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// doctrine issue, detailed explaination of comparative negligence
    /// ]]>
    /// </remarks>
    public class NgaLivYellowCabCompanyofCaliforniaTests
    {
        private readonly ITestOutputHelper output;

        public NgaLivYellowCabCompanyofCaliforniaTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void NgaLivYellowCabCompanyofCalifornia()
        {
            var test = new ComparativeNegligence<DrivingCar>(ExtensionMethods.Tortfeasor)
            {
                GetContribution = lp =>
                {
                    if (lp is NgaLi)
                        return new DrivingCar(1);
                    if (lp is YellowCabCompanyofCalifornia)
                        return new DrivingCar(2);
                    return new DrivingCar(0);
                }
            };

            var testResult = test.IsValid(new NgaLi(), new YellowCabCompanyofCalifornia());
            Assert.True(testResult);

            this.output.WriteLine(test.ToString());
        }
    }

    public class NgaLi : LegalPerson, IPlaintiff
    {
        public NgaLi(): base("Nga Li") { }
    }

    public class YellowCabCompanyofCalifornia : LegalPerson, ITortfeasor
    {
        public YellowCabCompanyofCalifornia(): base("Yellow Cab Company of California") { }
    }

    public class DrivingCar : IRankable
    {
        private readonly int _rank;

        public DrivingCar(int rank)
        {
            _rank = rank;
        }

        public int GetRank()
        {
            return _rank;
        }
    }
}
