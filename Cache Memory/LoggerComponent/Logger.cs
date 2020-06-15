using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerComponent
{
    public class Logger
    {
        public static string path = $"{System.IO.Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory)}/log.txt";
        public static void WriteLog(string msg, string className, string method)
        {
            if(String.IsNullOrWhiteSpace(msg) || String.IsNullOrWhiteSpace(className) || String.IsNullOrWhiteSpace(method))
            {
                throw new ArgumentException("Parameters cannot be empty,null or whitespace!");
            }
            //System.Diagnostics.Debug.WriteLine(path);
            using (StreamWriter sw = new StreamWriter(path,true))
            {
                sw.WriteLine("---Log Entry:");
                sw.WriteLine($"---{DateTime.Now.ToString()}");
                sw.WriteLine($"\tClass name: {className}");
                sw.WriteLine($"\tMethod name: {method}");
                sw.WriteLine($"\tMessage: {msg}");
                sw.WriteLine("-----------------------------------------");
            }
        }

        public static List<string> ReadLog()
        {
            List<string> log = new List<string>();
            if (!File.Exists(path))
            {
                var file = File.Create(path);
                file.Close();
                return log;
            }
            using (StreamReader sr = new StreamReader(path))
            {
                if (sr == null)
                    throw new ArgumentNullException("Log reader failed to open file!");

                string line = "";
                while ((line = sr.ReadLine()) != null) {    
                    log.Add(sr.ReadLine());
                }
            }
            return log;
        }

    }
}
