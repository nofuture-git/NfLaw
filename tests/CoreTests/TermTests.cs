using System;
using System.Collections.Generic;
using System.Linq;
using NoFuture.Law;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Tests
{
    
    public class TermTests
    {
        private static object something = new object();
        private readonly ITestOutputHelper output;

        public TermTests(ITestOutputHelper output)
        {
            this.output = output;
        }


        [Fact]
        public void TestExample()
        {
            
            var myTerm = new Term<object>("something's name", DBNull.Value);
            myTerm.As(new ColorGreen());
            myTerm.As(new SmoothSurface());
            myTerm.As(new HardSurface());
            myTerm.As(new SmallSize());
            myTerm.As(new RoundShape());

            Assert.True(myTerm.IsCategory(new SmallSize()));
            output.WriteLine(myTerm.ToString()); //something's name
            output.WriteLine(myTerm.GetCategory());

            var myTerm2 = new Term<object>("another name for same thing", DBNull.Value);
            Assert.False(myTerm2.Equals(myTerm));
            Assert.True(myTerm2.EqualRefersTo(myTerm));

        }

        [Fact]
        public void TestEquals()
        {
            var testSubj00 = new Term<object>("Swiss Coin Collection", new object());
            var testSubj01 = new Term<object>("Swiss Coin Collection", new object());

            Assert.True(testSubj00.Equals(testSubj01));
            Assert.False(testSubj00.EqualRefersTo(testSubj01));
            testSubj00 = new Term<object>("Swiss Coin Collection", something);
            testSubj01 = new Term<object>("Swiss Coin Collection", something);

            Assert.True(testSubj00.Equals(testSubj01));
            Assert.True(testSubj00.EqualRefersTo(testSubj01));

        }

        [Fact]
        public void TestSetOps()
        {
            var set00 = new SortedSet<Term<object>>
            {
                new Term<object>("Swiss Coin Collection", new object()),
                new Term<object>("Rarity Coin Collection", new object())
            };

            var set01 = new SortedSet<Term<object>>
            {
                new Term<object>("Swiss Coin Collection", new object())
            };

            var intersect = set00.Where(oo => set01.Any(ee => oo.Equals(ee))).ToList();
            
            Assert.True(intersect.Any());
            output.WriteLine(intersect.Count.ToString());

        }
    }

    public class ColorGreen : TermCategory
    {
        protected override string CategoryName => "green";
    }

    public class SmoothSurface : TermCategory
    {
        protected override string CategoryName => "smooth";
    }

    public class HardSurface : TermCategory
    {
        protected override string CategoryName => "hard";
    }

    public class SmallSize : TermCategory
    {
        protected override string CategoryName => "small";
    }

    public class RoundShape : TermCategory
    {
        protected override string CategoryName => "round";
    }
}
