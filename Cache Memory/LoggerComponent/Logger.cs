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
        public static void WriteLog(string msg, string className, string method)
        {
            if(String.IsNullOrWhiteSpace(msg) || String.IsNullOrWhiteSpace(className) || String.IsNullOrWhiteSpace(method))
            {
                throw new ArgumentException("Parameters cannot be empty,null or whitespace!");
            }

            using (StreamWriter sw = new StreamWriter("log.txt"))
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
            if (!File.Exists("log.txt"))
            {
                var file = File.Create("log.txt");
                file.Close();
                return log;
            }
            using (StreamReader sr = new StreamReader("log.txt"))
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
