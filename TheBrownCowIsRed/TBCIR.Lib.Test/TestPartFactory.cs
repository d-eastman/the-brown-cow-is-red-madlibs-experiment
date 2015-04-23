using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBCIR.Lib;

namespace TBCIR.Test
{
    /// <summary>
    /// Testable concrete class of PartFactory abstract class
    /// </summary>
    internal class TestPartFactory : PartFactory
    {
        public override Part GetPartBySymbol(string symbol)
        {
            if (symbol == "{TESTPART}")
                return new TestPart(this.GetValueBySymbol);
            else
                return null;
        }

        public override string GetValueBySymbol(string symbol)
        {
            if (symbol == "{TESTPART}")
                return "*VALUE OF " + symbol + "*";
            else
                return "";
        }

        public override List<string> SupportedSymbols
        {
            get 
            {
                List<string> ret = new List<string>();
                ret.Add("{TESTPART}");
                return ret;
            }
        }
    }
}
