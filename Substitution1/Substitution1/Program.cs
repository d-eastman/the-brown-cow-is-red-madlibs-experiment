using System;
using System.Collections.Generic;
using System.Text;
namespace Substitution1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("First silly prototype");

            Console.WriteLine("\nBuild by hand"); 
            for (int i = 1; i <= 5; i++)
            {
                List<IWord> words = new List<IWord>();
                words.Add(Literal.GetLiteral("The"));
                words.Add(Literal.Space());
                words.Add(Noun.RandomNoun());
                words.Add(Literal.Space()); 
                words.Add(Literal.GetLiteral("is"));
                words.Add(Literal.Space());
                words.Add(Adjective.RandomAdjective());
                words.Add(Literal.Period());
                IPhrasePattern p = new PhrasePattern(words);
                Console.WriteLine(i + ": " + p.Value);
            }

            Console.WriteLine("\nBuild by pattern");
            for (int i = 1; i <= 5; i++)
            {
                IPhrasePattern p = new PhrasePattern("The {ADJ} {N} and the {ADJ} {ADJ} {N} were {ADJ}.");
                Console.WriteLine(i + ": " + p.Value);
            }

            Console.WriteLine("\nBuild by pattern");             
            for (int i = 1; i <= 5; i++)
            {
                IPhrasePattern p = new PhrasePattern("{ADJ} {N} + {ADJ} {N} = {ADJ} {ADJ} {N}");
                Console.WriteLine(i + ": " + p.Value);
            }

            Console.WriteLine("\nBuild by dynamic pattern");
            for (int i = 1; i <= 5; i++)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("The");
                for (int j = 1; j <= i; j++ )
                    sb.Append(" {ADJ} {N} " + (j < i ? "and the" : ""));
                sb.Append(" changed from {ADJ} to {ADJ} as if the {ADJ} {N} was {ADJ}.");
                IPhrasePattern p = new PhrasePattern(sb.ToString());
                Console.WriteLine(i + ": " + p.Value);
            }

            Console.WriteLine("\nDone");
            Console.ReadLine();
        }
    }
}