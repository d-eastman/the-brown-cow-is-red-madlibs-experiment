using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBCIR.Lib
{
    public class Literal : Part
    {
        public Literal(string symbol, Func<string, string> getValueFunc)
            : base(symbol, getValueFunc)
        {
            _Value = symbol;
        }
        
        public Literal(string symbol)
            : this(symbol, null)
        {
        }

        public override string ToString()
        {
            return _Value;
        }

        public static Literal Get(string symbol)
        {
            return new Literal(symbol);
        }

    }
}
