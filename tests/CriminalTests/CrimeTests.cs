using System;
using System.Linq;
using NoFuture.Law;
using NoFuture.Law.Criminal.US;
using Xunit;
using Xunit.Abstractions;

namespace NoFuture.Law.Criminal.Tests
{
    public class CrimeTests
    {
        private readonly ITestOutputHelper output;

        public CrimeTests(ITestOutputHelper output)
        {
            this.output = output;
        }


        [Fact]
        public void TestCompareTo()
        {
            ICrime[] testSubjects =
            {
                new Misdemeanor(), new Infraction(),
                new Misdemeanor(), new Felony(),
                new Infraction(), new Felony(),
                new Infraction(), new Misdemeanor(),
            };

            Array.Sort(testSubjects);

            foreach(var c in testSubjects)
                this.output.WriteLine(c.GetType().Name);
            Assert.True(testSubjects.First() is Infraction);
            Assert.True(testSubjects.Last() is Felony);
        }

    }
}