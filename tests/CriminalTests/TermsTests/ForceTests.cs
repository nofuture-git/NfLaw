using NoFuture.Law.Criminal.US.Terms;
using NoFuture.Law.Criminal.US.Terms.Violence;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests.TermsTests
{
    public class ForceTests
    {
        private readonly ITestOutputHelper output;

        public ForceTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestGetCategoryRank()
        {
            var testSubject00 = new DeadlyForce();
            var testSubject01 = new NondeadlyForce();

            Assert.True(testSubject00.GetRank() > testSubject01.GetRank());
        }
    }
}
