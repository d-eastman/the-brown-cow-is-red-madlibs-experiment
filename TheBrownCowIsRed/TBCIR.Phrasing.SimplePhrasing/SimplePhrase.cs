using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using TBCIR.Lib;

namespace TBCIR.Phrasing.SimplePhrasing
{
    /// <summary>
    /// SimplePhrase uses a simple pattern constructed of independent tokens to
    /// generate a mix of literal text and randomly generated part values.
    /// </summary>
    public class SimplePhrase : Phrase
    {
        private string _Pattern = "";

        public string Pattern 
        { 
            get
            {
                return _Pattern;
            }
            private set
            {
                _Pattern = value;
                Parts = ParsePattern(_Pattern);
            }
        }

        public SimplePhrase(PartFactory partFactory, string pattern)
        {
            PartFactory = partFactory;
            Pattern = pattern; 
        }

        public override string GetRandomValue()
        {
            return GetRandomValue(Parts);
        }

        private List<Part> _Parts;

        protected List<Part> Parts
        {
            get
            {
                return _Parts;
            }
            private set
            {
                _Parts = value;
            }
        }

        protected string GetRandomValue(List<Part> parts)
        {
            StringBuilder ret = new StringBuilder();
            if (parts != null)
            {
                foreach (Part p in _Parts)
                {
                    ret.Append(p.Value);
                }
            }
            return ret.ToString();
        }

        protected List<Part> ParsePattern(string pattern)
        {
            List<Part> ret = new List<Part>();
            Regex regex = new Regex("{(N|ADJ)}", RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(pattern);
            List<int> cutpoints = new List<int>();
            cutpoints.Add(0);
            cutpoints.Add(pattern.Length - 1);
            for (int i = 0; i < matches.Count; i++)
            {
                int pos = matches[i].Index;
                int pos2 = matches[i].Index + matches[i].Length - 1;
                if (pos > 0)
                    cutpoints.Add(pos);
                if (pos2 < pattern.Length - 1)
                    cutpoints.Add(pos2);
                if (pos - 1 > 0)
                    cutpoints.Add(pos - 1);
                if (pos2 + 1 <= pattern.Length - 1)
                    cutpoints.Add(pos2 + 1);
            }
            cutpoints = cutpoints.OrderBy(x => x).ToList();
            for (int i = 0; i < cutpoints.Count; i += 2)
            {
                string token = pattern.Substring(cutpoints[i], cutpoints[i + 1] - cutpoints[i] + 1);
                Part part = PartFactory.GetPartBySymbol(token);
                ret.Add(part);
            }
            return ret;
        }
    }
}
