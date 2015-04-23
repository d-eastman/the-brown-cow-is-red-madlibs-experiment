using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TBCIR.Lib;

namespace TBCIR.Test
{
    /// <summary>
    /// Static methods are tested on the PartFactory class directly.
    /// Instance methods are tested on a implementation of PartFactory
    /// fabricated purely for testing purposed.
    /// </summary>
    [TestClass]
    public class PartFactory_Tests
    {
        [TestMethod]
        public void Static_GetPartFactory()
        {
            throw new NotImplementedException();
//            PartFactory a = PartFactory.GetPartFactory("TXT");
   //         Assert.IsNotNull(a);
  //          Assert.IsInstanceOfType(a, typeof(TextFilesPartFactory));
        }

        [TestMethod]
        public void Static_GetPartFactory_UnknownFactoryType()
        {
            PartFactory a = PartFactory.GetPartFactory("DOES NOT EXIST", "DOES NOT EXIST");
            Assert.IsNull(a);
        }

        [TestMethod]
        public void GetPartBySymbol()
        {
            string symbol = "{TESTPART}"; //This must be a valid supported symbol
            TestPartFactory f = new TestPartFactory();
            Assert.IsTrue(f.SupportedSymbols.Contains(symbol), "Results for the next assert are bogus since factory doesn't support this symbol");
            Part p = f.GetPartBySymbol(symbol);
            Assert.IsNotNull(p);
            Assert.IsInstanceOfType(p, typeof(TestPart));
        }

        [TestMethod]
        public void GetPartBySymbol_UnsupportedSymbol()
        {
            TestPartFactory f = new TestPartFactory();
            Part p = f.GetPartBySymbol("UNSUPPORTED SYMBOL");
            Assert.IsNull(p, "Null= " + (p == null).ToString());
        }

        [TestMethod]
        public void GetValueBySymbol()
        {
            string symbol = "{TESTPART}"; //This must be a valid supported symbol
            TestPartFactory f = new TestPartFactory();
            Assert.IsTrue(f.SupportedSymbols.Contains(symbol), "Results for the next assert are bogus since factory doesn't support this symbol");
            //f.GetValueBySymbol
        }

    }
}