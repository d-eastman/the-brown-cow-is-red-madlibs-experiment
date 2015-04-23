using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TBCIR.Phrasing.SimplePhrasing;
using TBCIR.Lib;

namespace TBCIR.Phrasing.SimplePhrasing.Test
{
    [TestClass]
    public class SimplePhrase_Tests
    {
        [TestMethod]
        public void Pattern()
        {
            SimplePhrase s = new SimplePhrase(new TestPartFactory(), "hello");
            Assert.AreEqual("hello", s.Pattern);

            s = new SimplePhrase(new TestPartFactory(), "The {TESTPART}.");
            Assert.AreEqual("The {TESTPART}.", s.Pattern);
        }

        [TestMethod]
        public void GetRandomValue_EmptyStringPattern()
        {
            SimplePhrase s = new SimplePhrase(new TestPartFactory(), "");
            Assert.AreEqual("", s.GetRandomValue());
        }

        [TestMethod]
        public void GetRandomValue_OneLiteralPart()
        {

        }

        [TestMethod]
        public void GetRandomValue_SimpleLiteralPattern()
        {
            SimplePhrase s = new SimplePhrase(new TestPartFactory(), "hello");
            Assert.AreEqual("hello", s.Pattern);
        }
    }
}
