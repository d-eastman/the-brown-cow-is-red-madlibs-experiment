using System;
using System.Collections.Generic;
namespace Substitution1
{
    public interface IWord
    {
        WordType Type { get; }

        string Value { get; }

        string PatternSymbol { get; }
    }

    public enum WordType
    {
        Literal,
        Adjective,
        Noun,
        Verb
    }

    public class Literal : IWord
    {
        public WordType Type
        {
            get { return WordType.Noun; }
        }

        private string _Value;

        public string Value
        {
            get { return _Value; }
        }

        public string PatternSymbol 
        {
            get { return ""; } 
        }

        public Literal(string value)
        {
            _Value = value;
        }

        public static Literal GetLiteral(string value)
        {
            return new Literal(value);
        }

        public static Literal Space()
        {
            return GetLiteral(" ");
        }

        public static Literal Period()
        {
            return GetLiteral(".");
        }
    }

    public class Noun : IWord
    {
        public WordType Type
        {
            get { return WordType.Noun; }
        }

        private string _Value;

        public string Value
        {
            get { return _Value; }
        }

        public string PatternSymbol
        {
            get { return "{N}"; }
        }

        public Noun(string value)
        {
            _Value = value;
        }

        public static Noun GetNoun(string value)
        {
            return new Noun(value);
        }

        private static Random RND = new Random();

        public static Noun RandomNoun()
        {
            List<string> list = new List<string>();
            list.Add("cow");
            list.Add("cat");
            list.Add("dog");
            list.Add("laptop");
            list.Add("airplane");
            list.Add("balloon");
            int index = RND.Next(0, list.Count);
            string value = list[index];
            return new Noun(value);
        }
    }

    public class Adjective : IWord
    {
        public WordType Type
        {
            get { return WordType.Noun; }
        }

        private string _Value;

        public string Value
        {
            get { return _Value; }
        }

        public string PatternSymbol
        {
            get { return "{ADJ}"; }
        }

        public Adjective(string value)
        {
            _Value = value;
        }

        public static Adjective GetAdjective(string value)
        {
            return new Adjective(value);
        }

        private static Random RND = new Random();

        public static Adjective RandomAdjective()
        {
            List<string> list = new List<string>();
            list.Add("brown");
            list.Add("red");
            list.Add("blue");
            list.Add("broken");
            list.Add("floating");
            list.Add("old");
            list.Add("new");
            int index = RND.Next(0, list.Count);
            string value = list[index];
            return new Adjective(value);
        }
    }
}