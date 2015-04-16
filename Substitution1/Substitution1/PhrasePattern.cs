using System;
using System.Collections.Generic;
using System.Text;

namespace Substitution1
{
    public interface IPhrasePattern
    {
        string Pattern { get; }

        List<IWord> Words { get; }

        string Value { get; }
    }

    public class PhrasePattern : IPhrasePattern
    {
        private string _Pattern;

        public string Pattern
        {
            get { return _Pattern; }
        }

        private List<IWord> _Words;

        public List<IWord> Words
        {
            get { return _Words; }
        }

        public string Value
        {
            get
            {
                StringBuilder ret = new StringBuilder();
                foreach (IWord w in _Words)
                    ret.Append(w.Value);
                return ret.ToString();
            }
        }

        public PhrasePattern(List<IWord> words)
        {
            _Words = words;
        }

        public PhrasePattern(string pattern)
        {
            _Pattern = pattern;
            _Words = ParsePattern(pattern);
        }

        /// <summary>
        /// Convert string representation of pattern into a list of IWords
        /// </summary>
        /// <param name="pattern">String pattern</param>
        /// <returns>List of IWords</returns>
        protected List<IWord> ParsePattern(string pattern)
        {
            List<IWord> ret = new List<IWord>();

            //string[] units = pattern.Split(' ');
            string[] units = pattern.Split(new Char[] { ' ', '.', ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < units.Length; i++)
            {
                string unit = units[i];
                if (unit.StartsWith("{") && unit.EndsWith("}"))
                {
                    switch (unit)
                    {
                        case "{N}":
                            ret.Add(Noun.RandomNoun());
                            break;
                        case "{ADJ}":
                            ret.Add(Adjective.RandomAdjective());
                            break;
                        default:
                            ret.Add(Literal.GetLiteral(unit)); //Unable to parse unknown symbol
                            break;
                    }
                }
                else
                {
                    ret.Add(Literal.GetLiteral(unit));
                }
                if (i < units.Length - 1)
                    ret.Add(Literal.Space()); //Add space unless this is the last unit
                else
                    ret.Add(Literal.Period());
            }

            return ret;
        }
    }
}