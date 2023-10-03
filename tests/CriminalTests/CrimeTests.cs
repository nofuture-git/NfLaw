﻿using System;
using System.Linq;
using NoFuture.Law;
using NoFuture.Law.Criminal.US;
using Xunit;

namespace NoFuture.Law.Criminal.Tests
{
    
    public class CrimeTests
    {

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
                Console.WriteLine(c.GetType().Name);
            Assert.IsTrue(testSubjects.First() is Infraction);
            Assert.IsTrue(testSubjects.Last() is Felony);
        }

    }
}