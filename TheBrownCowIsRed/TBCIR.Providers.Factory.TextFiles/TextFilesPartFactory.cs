using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TBCIR.Lib;

namespace TBCIR.Providers.Factory.TextFiles
{
    public class TextFilesPartFactory : PartFactory
    {
        private static TextFilesPartFactory _SingletonInstance;

        private static object lockObject = new object();

        protected static Dictionary<string, string> textFilesByPartType;

        private TextFilesPartFactory()
        {
        }

        public static TextFilesPartFactory GetSingletonInstance()
        {
            if (_SingletonInstance == null)
            {
                lock (lockObject)
                {
                    textFilesByPartType = new Dictionary<string, string>();
                    _SingletonInstance = new TextFilesPartFactory();

                    //TODO: pull this automatically from configuration file
                    _SingletonInstance.AddTextFile("{N}", @"C:\temp\TheBrownCowIsRed\TheBrownCowIsRed\TBCIR.TestConsoleClient\WordLists\www.talkenglish.com_Vocabulary_Top-1500-Nouns.txt")
                        .AddTextFile("{ADJ}", @"C:\temp\TheBrownCowIsRed\TheBrownCowIsRed\TBCIR.TestConsoleClient\WordLists\www.enchantedlearning.com_wordlist_adjectives.txt");
                }
            }
            return _SingletonInstance;
        }

        public TextFilesPartFactory AddTextFile(string symbol, string filename)
        {
            textFilesByPartType.Add(symbol, filename);
            return this;
        }

        public override Part GetPartBySymbol(string symbol)
        {
            Part ret;
            switch (symbol)
            {
                case "{ADJ}":
                    ret = new Adjective(this.GetValueBySymbol);
                    break;
                case "{N}":
                    ret = new Noun(this.GetValueBySymbol);
                    break;
                default:
                    ret = new Literal(symbol);
                    break;
            }
            return ret;
        }

        public override string GetValueBySymbol(string symbol)
        {
            return GetRandomWord(symbol);
        }

        private Dictionary<string, List<string>> _WordLists;

        protected List<string> GetWords(string symbol)
        {
            if (_WordLists == null)
                _WordLists = new Dictionary<string, List<string>>();

            if (!_WordLists.ContainsKey(symbol))
            {
                List<string> list = new List<string>();
                foreach (string filename in textFilesByPartType.Where(x => x.Key == symbol).Select(x => x.Value))
                {
                    using (StreamReader reader = new StreamReader(filename))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (!list.Contains(line.Trim()))
                            {
                                list.Add(line.Trim());
                            }
                        }
                    }
                }
                _WordLists.Add(symbol, list);
            }
            return _WordLists[symbol];
        }

        private Random RND = new Random();

        protected string GetRandomWord(string symbol)
        {
            List<string> words = GetWords(symbol);
            int next = RND.Next(0, words.Count);
            return words[next];
        }

        public override List<string> SupportedSymbols
        {
            get
            {
                List<string> ret = textFilesByPartType.Select(x => x.Key).Distinct().OrderBy(x => x).ToList();
                return ret;
            }
        }
    }
}