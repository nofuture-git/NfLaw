using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Tests
{
    public class TermCategoryTests
    {
        private readonly ITestOutputHelper output;

        public TermCategoryTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestIsRank()
        {
            var negOne = new NegOneRank();
            var zero = new ZeroRank();
            var one = new OneRank();

            Assert.True(TermCategory.IsRank(TermCategoryBoolOps.Eq, zero, zero));
            Assert.False(TermCategory.IsRank(TermCategoryBoolOps.Eq, one, zero));
            Assert.False(TermCategory.IsRank(TermCategoryBoolOps.Eq, zero, one));

            Assert.True(TermCategory.IsRank(TermCategoryBoolOps.Ne, zero, one));
            Assert.False(TermCategory.IsRank(TermCategoryBoolOps.Ne, one, one));

            Assert.True(TermCategory.IsRank(TermCategoryBoolOps.Gt, one, zero));
            Assert.True(TermCategory.IsRank(TermCategoryBoolOps.Gt, zero, negOne));

            Assert.True(TermCategory.IsRank(TermCategoryBoolOps.Lt, zero, one));
            Assert.True(TermCategory.IsRank(TermCategoryBoolOps.Lt, negOne, zero));

            Assert.True(TermCategory.IsRank(TermCategoryBoolOps.Ge, one, zero));
            Assert.True(TermCategory.IsRank(TermCategoryBoolOps.Ge, zero, negOne));
            Assert.True(TermCategory.IsRank(TermCategoryBoolOps.Ge, zero, zero));
            Assert.True(TermCategory.IsRank(TermCategoryBoolOps.Ge, negOne, negOne));
            Assert.True(TermCategory.IsRank(TermCategoryBoolOps.Ge, one, one));

            Assert.True(TermCategory.IsRank(TermCategoryBoolOps.Le, zero, one));
            Assert.True(TermCategory.IsRank(TermCategoryBoolOps.Le, negOne, zero));


        }
    }

    public class ZeroRank : TermCategory
    {
        public override int GetRank()
        {
            return 0;
        }
    }

    public class OneRank : TermCategory
    {
        public override int GetRank()
        {
            return 1;
        }
    }

    public class NegOneRank : TermCategory
    {
        public override int GetRank()
        {
            return -1;
        }
    }
}
