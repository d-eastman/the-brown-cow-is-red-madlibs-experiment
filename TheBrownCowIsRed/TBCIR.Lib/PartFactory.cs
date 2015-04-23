using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
namespace TBCIR.Lib
{
    public abstract class PartFactory
    {
        public abstract Part GetPartBySymbol(string symbol);

        public abstract string GetValueBySymbol(string symbol);

        public abstract List<string> SupportedSymbols { get; }

        /// <summary>
        /// "TBCIR.Providers.Factory.TextFiles.TextFilesPartFactory"
        /// "TBCIR.Providers.Factory.MsAccess.MsAccessPartFactory"
        /// </summary>
        /// <param name="dll"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public static PartFactory GetPartFactory(string dll, string className)
        {
            PartFactory ret = null;
            Assembly assem = null;
            try
            {
                assem = Assembly.LoadFile(dll);
            }
            catch
            {
                try
                {
                    assem = Assembly.LoadFile(AssemblyDirectory + @"\" + dll);
                }
                catch
                {
                    assem = Assembly.LoadFile(AssemblyDirectory + @"\" + dll + ".dll");
                }                
            }
            Type factoryType = assem.GetType(className);
            var instance = factoryType.InvokeMember("GetSingletonInstance", BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Static | BindingFlags.InvokeMethod, null, null, null);
            if (instance != null)
                ret = instance as PartFactory;
            return ret;
        }

        /// <summary>
        /// Find path of currently executing assembly (the EXE) so that relative paths can be resolved correctly
        /// Thank you stackoverflow: http://stackoverflow.com/questions/52797/how-do-i-get-the-path-of-the-assembly-the-code-is-in
        /// </summary>
        private static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
};