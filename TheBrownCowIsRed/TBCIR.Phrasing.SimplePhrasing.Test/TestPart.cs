using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBCIR.Lib;

namespace TBCIR.Phrasing.SimplePhrasing.Test
{
    internal class TestPart : Part
    {
        public TestPart(Func<string, string> getValueFunc)
            : base("{TESTPART}", getValueFunc)
        {
        }

        public static TestPart New(Func<string, string> getValueFunc)
        {
            return new TestPart(getValueFunc);
        }

        public static TestPart New()
        {
            return new TestPart(null);
        }

    }
}
