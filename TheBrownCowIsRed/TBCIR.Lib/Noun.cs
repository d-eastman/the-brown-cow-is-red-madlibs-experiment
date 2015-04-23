using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBCIR.Lib
{
    public class Noun : Part
    {
        public Noun(Func<string, string> getValueFunc)
            : base("{N}", getValueFunc)
        {
        }
    }
}
