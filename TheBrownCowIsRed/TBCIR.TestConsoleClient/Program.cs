using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using TBCIR.Lib;
using TBCIR.Phrasing.SimplePhrasing;

namespace TBCIR.TestConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            PartFactory f;
            f = PartFactory.GetPartFactory(@"TBCIR.Providers.Factory.TextFiles.dll", "TBCIR.Providers.Factory.TextFiles.TextFilesPartFactory");
            //f = PartFactory.GetPartFactory(@"TBCIR.Providers.Factory.MsAccess.dll", "TBCIR.Providers.Factory.MsAccess.MsAccessPartFactory");

            Console.WriteLine("Supported symbols: " + String.Join(" ", f.SupportedSymbols));

            Phrase p = new SimplePhrase(f, "The {ADJ} {N} is {ADJ} and {ADJ}.");
            Console.WriteLine(((SimplePhrase)p).Pattern);
            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine(p.GetRandomValue());
            }

            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
