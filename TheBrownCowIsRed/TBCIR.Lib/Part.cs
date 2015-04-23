using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBCIR.Lib
{
    public abstract class Part
    {
        protected string _Symbol;

        public string Symbol
        {
            get
            {
                return _Symbol;
            }
        }

        protected Func<string, string> _GetValueFunc;

        protected string _Value;

        public string Value
        {
            get
            {
                if (_GetValueFunc == null)
                    return _Value;
                else
                    return _GetValueFunc(Symbol);
            }
        }

        public Part(string symbol, Func<string, string> getValueFunc)
        {
            _Symbol = symbol;
            _GetValueFunc = getValueFunc;
        }

        public override string ToString()
        {
            return _Symbol;
        }
    }
}
