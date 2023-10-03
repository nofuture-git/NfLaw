using NoFuture.Law.Criminal.US.Terms;
using NoFuture.Law.Criminal.US.Terms.Violence;
using Xunit;

namespace NoFuture.Law.Criminal.Tests.TermsTests
{
    
    public class ForceTests
    {
        [Fact]
        public void TestGetCategoryRank()
        {
            var testSubject00 = new DeadlyForce();
            var testSubject01 = new NondeadlyForce();

            Assert.IsTrue(testSubject00.GetRank() > testSubject01.GetRank());
        }
    }
}
