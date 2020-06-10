using ReaderComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WriterComponent;

namespace Cache_Memory
{
    class Program
    {
        static void Main(string[] args)
        {
            Writer w = new Writer();

            w.SendToHistorical();
            Console.WriteLine("Writer je zavrsio!");
            Reader r = new Reader();

            r.Meni();

            Console.ReadLine();
        }
    }
}
