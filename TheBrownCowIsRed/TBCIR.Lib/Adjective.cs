using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBCIR.Lib
{
    public class Adjective : Part
    {
        public Adjective(Func<string, string> getValueFunc)
            : base("{ADJ}", getValueFunc)
        {
        }
    }
}
