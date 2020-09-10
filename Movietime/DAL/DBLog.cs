using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace DAL
{
    [ExcludeFromCodeCoverage]
    class DBLog
    {

        private static string LogFile = @"../Webapplikasjoner-Del-1-2018/Content/movietime-log.txt";
        

        public static void ErrorToFile(string msg, string name, Exception exp)
        {
            
            try
            {

                StreamWriter streamWriter = new System.IO.StreamWriter(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, LogFile), true);
                streamWriter.WriteLine("---------Error Log Start---------- on " + DateTime.Now + "<br>");
                streamWriter.WriteLine(name + "  --  " + msg + "  --  " + exp.Message + (exp.InnerException == null ? "" : ("  --  " + exp.InnerException.Message)));
                streamWriter.WriteLine("<br>---------Error Log End----------" + "<br><br>");
                streamWriter.Close();
            }
            catch (Exception)
            {

            }
        }
    }
}
