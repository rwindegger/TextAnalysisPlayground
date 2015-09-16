namespace TextAnalysis.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TokenTests
    {
        [TestMethod]
        public void TokenCompareToTest()
        {
            IComparable t1 = new Token { TokenStart = 10, TokenEnd = 20 };
            IComparable t2 = new Token { TokenStart = 10, TokenEnd = 20 };
            IComparable t3 = new Token { TokenStart = 5, TokenEnd = 20 };
            IComparable t4 = new Token { TokenStart = 15, TokenEnd = 20 };
            IComparable t5 = new Token { TokenStart = 10, TokenEnd = 15 };
            IComparable t6 = new Token { TokenStart = 10, TokenEnd = 25 };
            object t7 = new object();

            Assert.AreEqual(0, t1.CompareTo(t2));
            Assert.IsTrue(t1.CompareTo(t3) > 0);
            Assert.IsTrue(t1.CompareTo(t4) < 0);
            Assert.IsTrue(t1.CompareTo(t5) > 0);
            Assert.IsTrue(t1.CompareTo(t6) < 0);
            Assert.IsTrue(t1.CompareTo(t7) > 0);
        }

        [TestMethod]
        public void TokenToStringTest()
        {
            var t1 = new Token { TokenStart = 10, TokenEnd = 20, Content = "1234" };
            var expected = "10-20: 1234";
            var actual = t1.ToString();
            Assert.AreEqual(expected, actual);
        }
    }
}
