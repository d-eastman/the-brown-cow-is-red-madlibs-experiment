using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TBCIR.Lib;

namespace TBCIR.Test
{
    [TestClass]
    public class Part_Tests
    {
        //Note: constructor is adequately tested by other methods (with null and non-null values)
        //so not testing it separately

        [TestMethod]
        public void Symbol()
        {
            TestPart p = new TestPart(null);
            Assert.AreEqual("{TESTPART}", p.Symbol);
        }
        
        [TestMethod]
        public void Value()
        {
            TestPart p = new TestPart(null);
            Assert.IsNull(p.Value);
            p = new TestPart((s) => "hello '" + s + "'");
            Assert.AreEqual("hello", p.Value);
        }

        [TestMethod]
        public new void ToString()
        {
            TestPart p = new TestPart(null);
            Assert.AreEqual("{TESTPART}", p.ToString());
        }
    }
}
