using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBCIR.Lib;
using System.Data;
using System.Data.Odbc;

namespace TBCIR.Providers.Factory.MsAccess
{
    public class MsAccessPartFactory : PartFactory
    {
        private static MsAccessPartFactory _SingletonInstance;

        private static object lockObject = new object();

        protected static Dictionary<string, string> textFilesByPartType;

        private MsAccessPartFactory()
        {
        }

        public static MsAccessPartFactory GetSingletonInstance()
        {
            if (_SingletonInstance == null)
            {
                lock (lockObject)
                {
                    textFilesByPartType = new Dictionary<string, string>();
                    _SingletonInstance = new MsAccessPartFactory();

                    //TODO: pull this automatically from configuration file
                    _SingletonInstance.DBQ = @"C:\temp\TheBrownCowIsRed\TheBrownCowIsRed\TBCIR.TestConsoleClient\WordLists\Words.mdb";
                }
            }
            return _SingletonInstance;
        }

        private string _DBQ;
        public string DBQ
        {
            get { return _DBQ; }
            set { _DBQ = value; }
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

        private Random RND = new Random();

        /// <summary>
        /// This is actually random since .NET does the randomization. Relying on MS ACCESS to do the randomization always fails (with existing attempts anyway, commented out below).
        /// </summary>
        /// <param name="symbol">Part symbol</param>
        /// <returns>Random part value</returns>
        protected string GetRandomWord(string symbol)
        {
            string ret = "";

            using (OdbcConnection conn = new OdbcConnection(@"Driver={Microsoft Access Driver (*.mdb, *.accdb)};DBQ=" + DBQ + ";Uid=Admin;Pwd=;"))
            {
                string nakedSymbol = symbol.Replace("{", "").Replace("}", ""); //Symbols are coded in database without the curly braces
                string sql = "SELECT [ID] FROM [Values] WHERE [Symbol]='" + nakedSymbol + "'";
                int id = 0;
                using (OdbcDataAdapter adapter = new OdbcDataAdapter(sql, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int nextRand = RND.Next(0, dt.Rows.Count);
                        id = int.Parse(dt.Rows[nextRand]["ID"].ToString());
                    }
                }
                if (id > 0)
                {
                    sql = "SELECT [Value] FROM [Values] WHERE [ID]=" + id;
                    using (OdbcDataAdapter adapter = new OdbcDataAdapter(sql, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        if (dt != null && dt.Rows.Count == 1)
                        {
                            ret = dt.Rows[0]["Value"].ToString();
                        }
                    }
                }
            }

            return ret;
        }
        //NOT RANDOM
        //protected string GetRandomWord(string symbol)
        //{
        //    string ret = "";
        //    using (OdbcConnection conn = new OdbcConnection(@"Driver={Microsoft Access Driver (*.mdb, *.accdb)};DBQ=" + DBQ + ";Uid=Admin;Pwd=;"))
        //    {
        //        string nakedSymbol = symbol.Replace("{", "").Replace("}", "");
        //        string sql = "SELECT [Value] FROM [qryRandom" + nakedSymbol + "]";
        //        using (OdbcDataAdapter adapter = new OdbcDataAdapter(sql, conn))
        //        {
        //            DataTable dt = new DataTable();
        //            adapter.Fill(dt);
        //            if (dt != null && dt.Rows.Count == 1)
        //            {
        //                ret = dt.Rows[0]["Value"].ToString();
        //            }
        //        }
        //    }
        //    return ret;
        //}

        //NOT RANDOM
        //protected string GetRandomWord(string symbol)
        //{
        //    string ret = "";
        //    using (OdbcConnection conn = new OdbcConnection(@"Driver={Microsoft Access Driver (*.mdb, *.accdb)};DBQ=" + DBQ + ";Uid=Admin;Pwd=;"))
        //    {
        //        string nakedSymbol = symbol.Replace("{", "").Replace("}", "");
        //        string sql = "SELECT TOP 1 [Value] FROM [Values] WHERE [Symbol]='" + nakedSymbol + "' ORDER BY RND(" + DateTime.Now.Millisecond + ")";
        //        using (OdbcDataAdapter adapter = new OdbcDataAdapter(sql, conn))
        //        {
        //            DataTable dt = new DataTable();
        //            adapter.Fill(dt);
        //            if (dt != null && dt.Rows.Count > 0)
        //            {
        //                ret = dt.Rows[0]["Value"].ToString();
        //            }
        //        }
        //    }
        //    return ret;
        //}

        List<string> _SupportedSymbols = null;

        public override List<string> SupportedSymbols
        {
            get
            {
                if (_SupportedSymbols == null)
                {
                    _SupportedSymbols = new List<string>();

                    using (OdbcConnection conn = new OdbcConnection(@"Driver={Microsoft Access Driver (*.mdb, *.accdb)};DBQ=" + DBQ + ";Uid=Admin;Pwd=;"))
                    {
                        using (OdbcDataAdapter adapter = new OdbcDataAdapter("select Symbol from qrySupportedSymbols order by Symbol", conn))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    _SupportedSymbols.Add(dt.Rows[i]["Symbol"].ToString());
                                }
                            }
                        }
                    }
                }
                return _SupportedSymbols;
            }
        }
    }
}
