using ReaderComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WriterComponent;

namespace Cache_Memory
{
    public class Implementation
    {
        Writer w = new Writer();
        Reader r = new Reader();
        public void MenuChoise()
        {
            Console.WriteLine("1. Write data automatically (dumpingBuffer)");
            Console.WriteLine("2. Write data manually (direct to historical)");
            Console.WriteLine("3. Read data");
            Console.WriteLine("4. Exit app");
        }
    }
}
